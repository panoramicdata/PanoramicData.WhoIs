using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Enhancers.ProxyCurl;
using PanoramicData.WhoIs.Exceptions;
using PanoramicData.WhoIs.Test.Helpers;
using System.Net;
using System.Net.Mail;

namespace PanoramicData.WhoIs.Test;

public class ProxyCurlPersonEnhancerTests : TestBase
{
	private static readonly ProxyCurlConfig Config = new() { GoogleCx = "cx", GoogleKey = "g-key", ProxyCurlKey = "pc-key" };

	private static Person JanePerson() => new() { MailAddress = new MailAddress("jane.doe@acme.example") };

	private static (ProxyCurlPersonEnhancer Enhancer, StubHttpMessageHandler Handler) Create(
		Func<HttpRequestMessage, (HttpStatusCode, string)> responder,
		ProxyCurlConfig? config = null)
	{
		var handler = new StubHttpMessageHandler(responder);
		return (new ProxyCurlPersonEnhancer(config ?? Config, new HttpClient(handler)), handler);
	}

	[Fact]
	public async Task GetPersonFromLinkedInUrlAsync_MapsProfileAndSendsBearerKey()
	{
		var (enhancer, handler) = Create(r => ProxyCurlSamples.Route(r, "{}"));
		using var _ = enhancer;

		var result = (await enhancer.GetPersonFromLinkedInUrlAsync(JanePerson(), ProxyCurlSamples.LinkedInUrl, CancellationToken))!;

		handler.Requests.Should().ContainSingle();
		handler.Requests[0].RequestUri!.ToString().Should().Contain("url=" + ProxyCurlSamples.LinkedInUrl);
		handler.Requests[0].Headers.Authorization!.Parameter.Should().Be("pc-key");
		result.FirstName.Should().Be("Jane");
		result.Country.Should().Be("GB");
		result.City.Should().Be("London");
		result.BirthYear.Should().Be(1990);
		result.Languages.Should().BeEquivalentTo("English", "French");
		result.PersonalEmails.Should().ContainSingle().Which.Should().Be("jane@home.example");
		result.LinkedInUrl.Should().Be(ProxyCurlSamples.LinkedInUrl);
	}

	[Fact]
	public async Task EnhanceAsync_WithoutLinkedInUrl_SearchesGoogleFirst()
	{
		var google = ProxyCurlSamples.GoogleJson((ProxyCurlSamples.LinkedInUrl, "Jane Doe", "Acme"));
		var (enhancer, handler) = Create(r => ProxyCurlSamples.Route(r, google));
		using var _ = enhancer;

		await enhancer.EnhanceAsync(JanePerson(), CancellationToken);

		handler.Requests.Should().HaveCount(2);
		handler.Requests[0].RequestUri!.Host.Should().Be("customsearch.googleapis.com");
		handler.Requests[0].RequestUri!.Query.Should().Contain("cx=cx").And.Contain("key=g-key");
		handler.Requests[1].RequestUri!.ToString().Should().Contain(ProxyCurlSamples.LinkedInUrl);
	}

	[Fact]
	public async Task EnhanceAsync_WhenGoogleResultIsNotAProfile_FallsBackToEmailLookup()
	{
		var google = ProxyCurlSamples.GoogleJson(("https://example.com/page", "Jane Doe", "Acme"));
		var (enhancer, handler) = Create(r => ProxyCurlSamples.Route(r, google));
		using var _ = enhancer;

		await enhancer.EnhanceAsync(JanePerson(), CancellationToken);

		handler.Requests.Should().HaveCount(3);
		handler.Requests[1].RequestUri!.ToString().Should().Contain("work_email=jane.doe@acme.example");
		handler.Requests[2].RequestUri!.ToString().Should().Contain(ProxyCurlSamples.LinkedInUrl);
	}

	[Fact]
	public async Task SearchGoogleAsync_WithNoResults_ReturnsEmptyResponse()
	{
		var empty = """{"queries":{"request":[{"count":0}]}}""";
		var (enhancer, _) = Create(r => ProxyCurlSamples.Route(r, empty));
		using var _ = enhancer;

		var result = await enhancer.SearchGoogleAsync(JanePerson(), CancellationToken);

		result.Url.Should().BeEmpty();
		result.Score.Should().Be(0);
	}

	[Fact]
	public async Task SearchGoogleAsync_PicksHighestScoringResult()
	{
		var google = ProxyCurlSamples.GoogleJson(
			("https://example.com/a", "Other", "x"),
			(ProxyCurlSamples.LinkedInUrl, "Jane Doe", "x"));
		var (enhancer, _) = Create(r => ProxyCurlSamples.Route(r, google));
		using var _ = enhancer;
		var person = new Person { FirstName = "Jane", LastName = "Doe" };

		var result = await enhancer.SearchGoogleAsync(person, CancellationToken);

		result.Url.Should().Be(ProxyCurlSamples.LinkedInUrl);
		result.Score.Should().Be(75);
	}

	[Theory]
	[InlineData(HttpStatusCode.Unauthorized, "Invalid API Key")]
	[InlineData(HttpStatusCode.InternalServerError, "Invalid API Key")]
	[InlineData(HttpStatusCode.Forbidden, "You have run out of credits")]
	[InlineData(HttpStatusCode.NotFound, "The requested resource could not be found.")]
	[InlineData(HttpStatusCode.TooManyRequests, "Rate limited - please retry")]
	[InlineData(HttpStatusCode.ServiceUnavailable, "Enrichment failed, please retry.")]
	public async Task ReverseWorkEmailLookupAsync_WithHttpError_ThrowsProxyCurlException(HttpStatusCode status, string message)
	{
		var (enhancer, _) = Create(_ => (status, "{}"));
		using var _ = enhancer;

		var act = () => enhancer.ReverseWorkEmailLookupAsync("jane.doe@acme.example", CancellationToken);

		(await act.Should().ThrowAsync<ProxyCurlException>()).WithMessage(message);
	}

	[Fact]
	public async Task ReverseWorkEmailLookupAsync_WithUnmappedError_ReturnsEmptyString()
	{
		var (enhancer, _) = Create(_ => (HttpStatusCode.BadRequest, "{}"));
		using var _ = enhancer;

		var result = await enhancer.ReverseWorkEmailLookupAsync("jane.doe@acme.example", CancellationToken);

		result.Should().BeEmpty();
	}

	[Fact]
	public async Task GetPersonFromLinkedInUrlAsync_WithUnmappedError_ReturnsPersonWithUrlOnly()
	{
		var (enhancer, _) = Create(_ => (HttpStatusCode.BadRequest, "{}"));
		using var _ = enhancer;

		var result = await enhancer.GetPersonFromLinkedInUrlAsync(JanePerson(), ProxyCurlSamples.LinkedInUrl, CancellationToken);

		result!.LinkedInUrl.Should().Be(ProxyCurlSamples.LinkedInUrl);
		result.FirstName.Should().BeNull();
	}

	[Fact]
	public async Task GetPersonFromLinkedInUrlAsync_WithoutMailAddress_ReturnsSamePersonWithoutRequests()
	{
		var (enhancer, handler) = Create(r => ProxyCurlSamples.Route(r, "{}"));
		using var _ = enhancer;
		var person = new Person();

		var result = await enhancer.GetPersonFromLinkedInUrlAsync(person, ProxyCurlSamples.LinkedInUrl, CancellationToken);

		result.Should().BeSameAs(person);
		handler.Requests.Should().BeEmpty();
	}

	[Fact]
	public async Task GetPersonFromLinkedInUrlAsync_WithNullArguments_Throws()
	{
		var (enhancer, _) = Create(r => ProxyCurlSamples.Route(r, "{}"));
		using var _ = enhancer;

		var noPerson = () => enhancer.GetPersonFromLinkedInUrlAsync(null!, "u", CancellationToken);
		var noUrl = () => enhancer.GetPersonFromLinkedInUrlAsync(new Person(), null!, CancellationToken);

		await noPerson.Should().ThrowAsync<ArgumentNullException>();
		await noUrl.Should().ThrowAsync<ArgumentNullException>();
	}

	[Fact]
	public async Task GetPersonFromLinkedInUrlAsync_WithCachedFile_DoesNotCallTheApi()
	{
		var folder = Directory.CreateTempSubdirectory();
		try
		{
			await File.WriteAllTextAsync(Path.Combine(folder.FullName, "email_jane.doe@acme.example.json"), ProxyCurlSamples.ProfileJson, CancellationToken);
			var config = new ProxyCurlConfig { ProxyCurlCacheFolder = folder.FullName };
			var (enhancer, handler) = Create(r => ProxyCurlSamples.Route(r, "{}"), config);
			using var _ = enhancer;

			var result = await enhancer.GetPersonFromLinkedInUrlAsync(JanePerson(), ProxyCurlSamples.LinkedInUrl, CancellationToken);

			result!.LastName.Should().Be("Doe");
			handler.Requests.Should().BeEmpty();
		}
		finally
		{
			folder.Delete(recursive: true);
		}
	}

	[Fact]
	public async Task Dispose_DisposesTheSuppliedClient()
	{
		var (enhancer, handler) = Create(r => ProxyCurlSamples.Route(r, "{}"));

		enhancer.Dispose();
		var result = await enhancer.ReverseWorkEmailLookupAsync("a@b.example", CancellationToken);

		result.Should().BeEmpty();
		handler.Requests.Should().BeEmpty();
	}

	[Fact]
	public void DefaultConstructor_CreatesAUsableEnhancer()
	{
		using var enhancer = new ProxyCurlPersonEnhancer(Config);

		enhancer.CompanyEnhancers.Should().BeEmpty();
	}
}
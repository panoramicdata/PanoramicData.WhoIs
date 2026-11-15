using PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;
using PanoramicData.WhoIs.Test.Helpers;

namespace PanoramicData.WhoIs.Test;

public class GoogleSearcherTests : TestBase
{
	private static readonly Person Jane = new() { FirstName = "Jane", LastName = "Doe", Company = new Company { Name = "Acme" } };

	private static (GoogleSearcher Searcher, StubHttpMessageHandler Handler) Create(string googleBody)
	{
		var handler = new StubHttpMessageHandler(r => ProxyCurlSamples.Route(r, googleBody));
		return (new GoogleSearcher("cx", "g-key", "pc-key", new HttpClient(handler)), handler);
	}

	[Fact]
	public async Task SearchGoogleAsync_PicksHighestScoringResult()
	{
		var (searcher, _) = Create(ProxyCurlSamples.GoogleJson(
			("https://example.com/a", "Other", "x"),
			(ProxyCurlSamples.LinkedInUrl, "Jane Doe", "Acme")));
		using var _ = searcher;

		var result = await searcher.SearchGoogleAsync(Jane, CancellationToken);

		result.Url.Should().Be(ProxyCurlSamples.LinkedInUrl);
		result.Title.Should().Be("Jane Doe");
		result.Score.Should().Be(100);
	}

	[Fact]
	public async Task SearchGoogleAsync_WithNoResults_ReturnsEmptyResponse()
	{
		var (searcher, _) = Create("""{"queries":{"request":[{"count":0}]}}""");
		using var _ = searcher;

		var result = await searcher.SearchGoogleAsync(Jane, CancellationToken);

		result.Url.Should().BeEmpty();
	}

	[Fact]
	public async Task SearchLinkedInAsync_WithProfileResult_MapsTheFetchedProfile()
	{
		var (searcher, handler) = Create(ProxyCurlSamples.GoogleJson((ProxyCurlSamples.LinkedInUrl, "Jane Doe", "Acme")));
		using var _ = searcher;

		var result = await searcher.SearchLinkedInAsync(Jane, CancellationToken);

		handler.Requests.Should().HaveCount(2);
		handler.Requests[1].Headers.Authorization!.Parameter.Should().Be("pc-key");
		result.Occupation.Should().Be("Engineer");
		result.Country.Should().Be("United Kingdom");
		result.Age.Should().Be(DateTime.Now.Year - 1990);
		result.Experiences.Should().ContainSingle().Which.Company.Should().Be("Acme");
		result.Education.Should().ContainSingle().Which.School.Should().Be("Uni");
		result.Awards.Should().ContainSingle().Which.Issuer.Should().Be("Body");
		result.Courses.Should().ContainSingle().Which.Number.Should().Be("C1");
		result.Projects.Should().ContainSingle().Which.Title.Should().Be("Project");
	}

	[Fact]
	public async Task SearchLinkedInAsync_WithNonProfileResult_ResolvesTheUrlFirst()
	{
		var (searcher, handler) = Create(ProxyCurlSamples.GoogleJson(("https://example.com/page", "Jane Doe", "Acme")));
		using var _ = searcher;

		await searcher.SearchLinkedInAsync(Jane, CancellationToken);

		handler.Requests.Should().HaveCount(3);
		handler.Requests[1].RequestUri!.ToString().Should().Contain("resolve/email");
		handler.Requests[2].RequestUri!.ToString().Should().Contain(ProxyCurlSamples.LinkedInUrl);
	}

	[Fact]
	public async Task SearchLinkedInAsync_WithNullPerson_Throws()
	{
		var (searcher, _) = Create("{}");
		using var _ = searcher;

		var act = () => searcher.SearchLinkedInAsync(null!, CancellationToken);

		await act.Should().ThrowAsync<ArgumentNullException>();
	}

	[Fact]
	public void DefaultConstructor_CreatesAUsableSearcher()
	{
		var act = () => new GoogleSearcher("cx", "g-key", "pc-key").Dispose();

		act.Should().NotThrow();
	}
}
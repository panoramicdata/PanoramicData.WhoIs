using System.Net;

namespace PanoramicData.WhoIs.Test.Helpers;

internal sealed class StubHttpMessageHandler(Func<HttpRequestMessage, (HttpStatusCode Status, string Body)> responder) : HttpMessageHandler
{
	public List<HttpRequestMessage> Requests { get; } = [];

	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		Requests.Add(request);
		var (status, body) = responder(request);
		return Task.FromResult(new HttpResponseMessage(status) { Content = new StringContent(body) });
	}
}
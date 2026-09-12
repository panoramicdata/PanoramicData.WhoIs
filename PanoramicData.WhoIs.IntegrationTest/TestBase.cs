using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.IntegrationTest.Helpers;
using System.Net.Mail;

namespace PanoramicData.WhoIs.IntegrationTest;

public abstract class TestBase
{
	protected ProxyCurlPersonEnhancer ProxyCurlPersonEnhancer { get; }

	protected PersonEnhancer PersonEnhancer { get; }

	protected MailAddress ValidMailAddress { get; }

	protected string ValidFirstName { get; }

	protected string ValidProfileUrl { get; }

	protected static CancellationToken CancellationToken => TestContext.Current.CancellationToken;

	protected TestBase()
	{
		var appSettings = TestConfiguration.LoadAppSettings();

		ValidMailAddress = new(appSettings.ValidEmailAddress);
		ValidFirstName = appSettings.ValidFirstName;
		ValidProfileUrl = appSettings.ValidProfileUrl;

		ProxyCurlPersonEnhancer = new ProxyCurlPersonEnhancer(appSettings.ToProxyCurlConfig());

		PersonEnhancer = new PersonEnhancerBuilder()
			.WithProxyCurlEnhancer()
			.Build();
	}
}

using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Interfaces;

namespace PanoramicData.WhoIs.IntegrationTest.Helpers;

internal class PersonEnhancerBuilder
{
	private readonly List<IPersonEnhancer> _personEnhancers = new(10);
	private readonly AppSettings _appSettings = TestConfiguration.LoadAppSettings();

	public PersonEnhancerBuilder WithProxyCurlEnhancer()
	{
		_personEnhancers.Add(new ProxyCurlPersonEnhancer(_appSettings.ToProxyCurlConfig()));
		return this;
	}

	public PersonEnhancer Build() => new(_personEnhancers);
}

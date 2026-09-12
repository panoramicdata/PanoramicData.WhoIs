using Microsoft.Extensions.Configuration;
using PanoramicData.WhoIs.Enhancers.ProxyCurl;

namespace PanoramicData.WhoIs.IntegrationTest.Helpers;

/// <summary>
/// Loads the integration test settings from the appsettings.json alongside the project file.
/// </summary>
internal static class TestConfiguration
{
	internal static AppSettings LoadAppSettings()
	{
		// The test assembly runs from bin/<configuration>/<framework>, so the project directory
		// - where appsettings.json lives - is three levels up.
		var currentDirectoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
		var rootDirectoryInfo = currentDirectoryInfo.Parent?.Parent?.Parent
			?? throw new InvalidOperationException("Failed to identify root directory for this project!");

		var configuration = new ConfigurationBuilder()
			.SetBasePath(rootDirectoryInfo.FullName)
			.AddJsonFile("appsettings.json")
			.Build();

		return configuration
			.GetSection("AppSettings")
			.Get<AppSettings>() ?? throw new InvalidOperationException("Failed to load appsettings.json");
	}

	internal static ProxyCurlConfig ToProxyCurlConfig(this AppSettings appSettings) => new()
	{
		GoogleCx = appSettings.GoogleCx ?? string.Empty,
		GoogleKey = appSettings.GoogleKey ?? string.Empty,
		ProxyCurlKey = appSettings.ProxyCurlKey ?? string.Empty,
		ProxyCurlCacheFolder = appSettings.ProxyCurlCacheFolder ?? string.Empty
	};
}

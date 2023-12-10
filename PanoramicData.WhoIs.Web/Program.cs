using PanoramicData.WhoIs.Extensions;
using PanoramicData.WhoIs.ProxyCurl;

namespace PanoramicData.HumanWhoIs.Demo.Web;

public static class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var proxyCurlConfig = builder.Configuration
			.GetSection("ProxyCurlConfig")
			.Get<ProxyCurlConfig>() ?? throw new Exception("Unable to deserialize Appsettings.");

		builder.Services.AddWhoIs(opt => opt
			.AddProxyCurl(
				proxyCurlConfig.GoogleCx,
				proxyCurlConfig.GoogleKey,
				proxyCurlConfig.ProxyCurlKey)
			.AddWhoIs()
			);

		// Add services to the container.
		builder.Services.AddRazorPages();
		builder.Services.AddServerSideBlazor();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseStaticFiles();

		app.UseRouting();

		app.MapBlazorHub();
		app.MapFallbackToPage("/_Host");

		app.Run();
	}
}
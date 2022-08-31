using EmailLookup.Core.Configuration;

namespace EmailLookup.Demo.Web;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// TODO: Replace with lookup of values from config
		builder.Services.AddEmailLookup(opt => opt
			.AddProxyCurl("1501e4a4050ced422", "AIzaSyAyujHqqnhB8SAdn2DsmuWD75PfzlXbfdY", "jRnkhB5kx8UwrdNRDltJtg")
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
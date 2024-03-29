using LocalLangLibrary.DataAccess;
using LocalLangLibrary.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDbAccess.DataAccess.Abstractions;

namespace LocalLangUI
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging(builder =>
			{
				builder.AddConfiguration(Configuration.GetRequiredSection("Logging"));
				builder.AddConsole();
				builder.AddDebug();
			});

			services.AddRazorPages();
			services.AddServerSideBlazor();
			services.AddMemoryCache();

			services.AddSingleton<IDbConnection, MongoDbConnection>();
			services.AddSingleton<ICategoryCollection, MongoCategoryCollection>();
			services.AddSingleton<IExpressionCollection, MongoExpressionCollection>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}

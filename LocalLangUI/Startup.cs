using LocalLangLibrary.DataAccess;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;

namespace LocalLangUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMemoryCache();

            //services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApp(Configuration, configSectionName: "AzureAdB2C");

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Admin", policy =>
            //    {
            //        policy.RequireClaim("jobTitle", "Admin");
            //    });
            //});

            services.AddSingleton<IDbConnection, DbConnection>();
            services.AddSingleton<ICategoryData, MongoCategoryData>();
            services.AddSingleton<IStatusData, MongoStatusData>();
            services.AddSingleton<IUserData, MongoUserData>();
            services.AddSingleton<IExpressionData, MongoExpressionData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            //app.UseRewriter(new RewriteOptions().Add(context =>
            //{
            //    if (context.HttpContext.Request.Path == "/MicrosoftIdentity/Account/SignedOut")
            //    {
            //        context.HttpContext.Response.Redirect("/");
            //    }
            //}));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LocalLangUI
{
	public class Program
	{
		public static void Main(string[] args)
		{

			var builder = Host.CreateDefaultBuilder(args)
							.ConfigureWebHostDefaults(hostBuilder =>
							{
								hostBuilder.UseStartup<Startup>();
							});

			builder.Build().Run();
		}
	}
}

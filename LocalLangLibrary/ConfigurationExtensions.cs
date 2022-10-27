using Microsoft.Extensions.Configuration;

namespace LocalLangLibrary
{
	public static class ConfigurationExtensions
	{
		public const string DatabaseNameSection = "DatabaseName";
		public const string ConnectionId = "MongoDB";

		public const string DateFormat = "yyyy-MM-dd";

		public static string GetDatabaseName(this IConfiguration configuration)
		{
			return configuration[DatabaseNameSection];
		}

		public static string GetConnectionString(this IConfiguration configuration)
		{
			return configuration.GetConnectionString(ConnectionId);
		}
	}
}

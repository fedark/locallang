using LocalLangLibrary.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace LocalLangLibrary.DataAccess
{
	public class DbConnection : IDbConnection
	{
		public string DbName { get; private set; }
		public MongoClient Client { get; private set; }

		public string CategoryCollectionName { get; private set; } = "category";
		public string ExpressionCollectionName { get; private set; } = "expression";

		public IMongoCollection<Category> CategoryCollection { get; private set; }
		public IMongoCollection<Expression> ExpressionCollection { get; private set; }

		public DbConnection(IConfiguration config)
		{
			DbName = config.GetDatabaseName();
			Client = new MongoClient(config.GetConnectionString());
			var db = Client.GetDatabase(DbName);

			CategoryCollection = db.GetCollection<Category>(CategoryCollectionName);
			ExpressionCollection = db.GetCollection<Expression>(ExpressionCollectionName);
		}
	}
}

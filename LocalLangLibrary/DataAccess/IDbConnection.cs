using LocalLangLibrary.Models;
using MongoDB.Driver;

namespace LocalLangLibrary.DataAccess
{
	public interface IDbConnection
	{
		IMongoCollection<Category> CategoryCollection { get; }
		string CategoryCollectionName { get; }
		IMongoCollection<Expression> ExpressionCollection { get; }
		string ExpressionCollectionName { get; }
		string DbName { get; }
		MongoClient Client { get; }
	}
}
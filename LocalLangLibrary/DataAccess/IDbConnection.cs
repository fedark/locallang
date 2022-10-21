using LocalLangLibrary.Models;
using MongoDB.Driver;

namespace LocalLangLibrary.DataAccess
{
    public interface IDbConnection
    {
        MongoClient Client { get; }
        string DbName { get; }

        string CategoryCollectionName { get; }
        string StatusCollectionName { get; }
        string UserCollectionName { get; }
        string ExpressionCollectionName { get; }

        IMongoCollection<CategoryModel> CategoryCollection { get; }
        IMongoCollection<StatusModel> StatusCollection { get; }
        IMongoCollection<UserModel> UserCollection { get; }
        IMongoCollection<ExpressionModel> ExpressionCollection { get; }
    }
}
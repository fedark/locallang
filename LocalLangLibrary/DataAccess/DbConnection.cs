using LocalLangLibrary.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace LocalLangLibrary.DataAccess
{
    public class DbConnection : IDbConnection
    {
        private readonly IConfiguration config_;
        private readonly IMongoDatabase db_;
        private string connectionId_ = "MongoDB";

        public string DbName { get; private set; }
        public string CategoryCollectionName { get; private set; } = "categories";
        public string StatusCollectionName { get; private set; } = "statuses";
        public string UserCollectionName { get; private set; } = "users";
        public string ExpressionCollectionName { get; private set; } = "expressions";
        public MongoClient Client { get; private set; }
        public IMongoCollection<CategoryModel> CategoryCollection { get; private set; }
        public IMongoCollection<StatusModel> StatusCollection { get; private set; }
        public IMongoCollection<UserModel> UserCollection { get; private set; }
        public IMongoCollection<ExpressionModel> ExpressionCollection { get; private set; }

        public DbConnection(IConfiguration config)
        {
            config_ = config;
            Client = new MongoClient(config_.GetConnectionString(connectionId_));
            DbName = config_["DatabaseName"];
            db_ = Client.GetDatabase(DbName);

            CategoryCollection = db_.GetCollection<CategoryModel>(CategoryCollectionName);
            StatusCollection = db_.GetCollection<StatusModel>(StatusCollectionName);
            UserCollection = db_.GetCollection<UserModel>(UserCollectionName);
            ExpressionCollection = db_.GetCollection<ExpressionModel>(ExpressionCollectionName);
        }
    }
}

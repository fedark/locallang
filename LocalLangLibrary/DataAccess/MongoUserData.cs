using LocalLangLibrary.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public class MongoUserData : IUserData
	{
		private readonly IMongoCollection<UserModel> users_;

		public MongoUserData(IDbConnection db)
		{
			users_ = db.UserCollection;
		}

		public async Task<IList<UserModel>> GetUsersAsync()
		{
			var result = await users_.FindAsync(_ => true);
			return result.ToList();
		}

		public async Task<UserModel> GetUserAsync(string id)
		{
			var result = await users_.FindAsync(u => u.Id == id);
			return result.FirstOrDefault();
		}
		public async Task<UserModel> GetUserFromAuthentificationAsync(string objectId)
		{
			var result = await users_.FindAsync(u => u.ObjectIdentifier == objectId);
			return result.FirstOrDefault();
		}

		public Task CreateUser(UserModel user)
		{
			return users_.InsertOneAsync(user);
		}

		public Task UpdateUser(UserModel user)
		{
			//var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
			return users_.ReplaceOneAsync(u => u.Id == user.Id, user, new ReplaceOptions { IsUpsert = true });
		}
	}
}

using LocalLangLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public interface IUserData
	{
		Task CreateUser(UserModel user);
		Task<UserModel> GetUserAsync(string id);
		Task<UserModel> GetUserFromAuthentificationAsync(string objectId);
		Task<IList<UserModel>> GetUsersAsync();
		Task UpdateUser(UserModel user);
	}
}
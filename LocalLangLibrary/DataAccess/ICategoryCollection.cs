using LocalLangLibrary.Models;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public interface ICategoryCollection : IDbCollection<Category>
	{
		Task<Category> GetOrDefaultAsync(string? id = null);
	}
}

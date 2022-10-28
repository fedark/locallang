using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public interface IDbCollection<TDocument>
	{
		Task Create(TDocument collection);
		Task<TDocument?> GetAsync(string id);
		Task<IList<TDocument>?> GetAllAsync();
		Task UpdateAsync(TDocument collection);
	}
}
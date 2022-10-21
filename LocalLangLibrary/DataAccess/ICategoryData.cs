using LocalLangLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public interface ICategoryData
	{
		Task CreateCategory(CategoryModel category);
		Task<IList<CategoryModel>> GetCategoriesAsync();
	}
}
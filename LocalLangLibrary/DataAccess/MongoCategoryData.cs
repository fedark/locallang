using LocalLangLibrary.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public class MongoCategoryData : ICategoryData
	{
		private readonly IMongoCollection<CategoryModel> categories_;
		private readonly IMemoryCache cache_;
		private const string CacheName = "CategoryData";

		public MongoCategoryData(IDbConnection db, IMemoryCache cache)
		{
			categories_ = db.CategoryCollection;
			cache_ = cache;
		}

		public async Task<IList<CategoryModel>> GetCategoriesAsync()
		{
			var result = cache_.Get<IList<CategoryModel>>(CacheName);
			if (result is null)
			{
				var allCategories = await categories_.FindAsync(_ => true);
				result = allCategories.ToList();

				cache_.Set(CacheName, result, TimeSpan.FromDays(1));
			}

			return result;
		}

		public Task CreateCategory(CategoryModel category)
		{
			return categories_.InsertOneAsync(category);
		}
	}
}

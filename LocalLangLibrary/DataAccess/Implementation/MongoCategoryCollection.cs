using LocalLangLibrary.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public class MongoCategoryCollection : ICategoryCollection
	{
		private const string CacheName = "CategoryData";
		private readonly IMemoryCache cache_;

		private Category? defaultCategory_;
		private readonly IMongoCollection<Category> categories_;

		public MongoCategoryCollection(IDbConnection db, IMemoryCache cache)
		{
			categories_ = db.CategoryCollection;
			cache_ = cache;
		}

		public Task Create(Category category)
		{
			return categories_.InsertOneAsync(category);
		}

		public async Task<Category?> GetAsync(string id)
		{
			var result = await categories_.FindAsync(e => e.Id == id);
			return result.FirstOrDefault();
		}

		public async Task<IList<Category>?> GetAllAsync()
		{
			var result = cache_.Get<IList<Category>?>(CacheName);
			if (result is null)
			{
				var allCategories = await categories_.FindAsync(_ => true);
				result = await allCategories.ToListAsync();

				cache_.Set(CacheName, result, TimeSpan.FromDays(1));
			}

			return result;
		}

		public async Task UpdateAsync(Category category)
		{
			await categories_.ReplaceOneAsync(e => e.Id == category.Id, category);
			cache_.Remove(CacheName);
		}
	}
}

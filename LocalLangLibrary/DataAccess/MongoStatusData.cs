using LocalLangLibrary.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public class MongoStatusData : IStatusData
	{
		private readonly IMongoCollection<StatusModel> statuses_;
		private readonly IMemoryCache cache_;
		private const string CacheName = "StatusData";

		public MongoStatusData(IDbConnection db, IMemoryCache cache)
		{
			statuses_ = db.StatusCollection;
			cache_ = cache;
		}

		public async Task<IList<StatusModel>> GetStatusesAsync()
		{
			var result = cache_.Get<IList<StatusModel>>(CacheName);
			if (result is null)
			{
				var allStatuses = await statuses_.FindAsync(_ => true);
				result = allStatuses.ToList();

				cache_.Set(CacheName, result, TimeSpan.FromDays(1));
			}

			return result;
		}

		public Task CreateStatus(StatusModel status)
		{
			return statuses_.InsertOneAsync(status);
		}
	}
}

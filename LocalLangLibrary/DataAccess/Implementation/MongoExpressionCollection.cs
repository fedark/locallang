using LocalLangLibrary.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public class MongoExpressionCollection : IExpressionCollection
	{
		private const string CacheName = "ExpressionData";
		private readonly IMemoryCache cache_;

		private readonly IDbConnection dbConnection_;
		private readonly IMongoCollection<Expression> expressions_;

		public MongoExpressionCollection(IDbConnection dbConnection, IMemoryCache cache)
		{
			dbConnection_ = dbConnection;
			cache_ = cache;
			expressions_ = dbConnection_.ExpressionCollection;
		}

		public async Task<IList<Expression>?> GetAllAsync()
		{
			var result = await GetNotRejectedAsync();
			return result?.Where(e => e.Status == Status.Approved).ToList();
		}

		public async Task<Expression?> GetAsync(string id)
		{
			try
			{
				var result = await expressions_.FindAsync(e => e.Id == id);
				return result.FirstOrDefault();
			}
			catch (Exception)
			{
				// log
				return null;
			}
		}

		public Task Create(Expression expression)
		{
			return expressions_.InsertOneAsync(expression);
		}

		public async Task UpdateAsync(Expression expression)
		{
			await expressions_.ReplaceOneAsync(e => e.Id == expression.Id, expression);
			cache_.Remove(CacheName);
		}

		public async Task<IList<Expression>?> GetPendingAsync()
		{
			var result = await GetNotRejectedAsync();
			return result?.Where(e => e.Status == Status.Pending).ToList();
		}

		public async Task LikeAsync(string id)
		{
			var expression = await GetAsync(id);
			if (expression is not null)
			{
				expression.Likes++;
				await expressions_.ReplaceOneAsync(e => e.Id == id, expression);
				cache_.Remove(CacheName);
			}

			//TODO: log
		}

		private async Task<IList<Expression>?> GetNotRejectedAsync()
		{
			var result = cache_.Get<IList<Expression>?>(CacheName);
			if (result is null)
			{
				var allExpressions = await expressions_.FindAsync(e => e.Status != Status.Rejected);
				result = await allExpressions.ToListAsync();

				cache_.Set(CacheName, result, TimeSpan.FromMinutes(1));
			}

			return result;
		}
	}
}

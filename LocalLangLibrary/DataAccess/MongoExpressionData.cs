using LocalLangLibrary.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public class MongoExpressionData : IExpressionData
	{
		private readonly IDbConnection dbConnection_;
		private readonly IUserData userData_;
		private readonly IMemoryCache cache_;
		private readonly IMongoCollection<ExpressionModel> expressions_;
		private const string CacheName = "ExpressionData";

		public MongoExpressionData(IDbConnection dbConnection, IUserData userData, IMemoryCache cache)
		{
			dbConnection_ = dbConnection;
			userData_ = userData;
			cache_ = cache;
			expressions_ = dbConnection_.ExpressionCollection;
		}

		public async Task<IList<ExpressionModel>> GetExpressionsAsync()
		{
			var result = cache_.Get<IList<ExpressionModel>>(CacheName);
			if (result is null)
			{
				var allExpressions = await expressions_.FindAsync(e => !e.Archived);
				result = allExpressions.ToList();

				cache_.Set(CacheName, result, TimeSpan.FromMinutes(1));
			}

			return result;
		}

		public async Task<IList<ExpressionModel>> GetUserExpressionsAsync(string userId)
		{
			var result = cache_.Get<IList<ExpressionModel>>(userId);
			if (result is null)
			{
				var userExpressions = await expressions_.FindAsync(e => e.Author.Id == userId);
				result = userExpressions.ToList();

				cache_.Set(userId, result, TimeSpan.FromMinutes(1));
			}

			return result;
		}

		public async Task<IList<ExpressionModel>> GetApprovedExpressionsAsync()
		{
			var result = await GetExpressionsAsync();
			return result.Where(e => e.ApprovedForRelease).ToList();
		}

		public async Task<IList<ExpressionModel>> GetWaitingExpressionsAsync()
		{
			var result = await GetExpressionsAsync();
			return result.Where(e => !e.ApprovedForRelease && !e.Rejected).ToList();
		}

		public async Task<ExpressionModel> GetExpressionAsync(string id)
		{
			var result = await expressions_.FindAsync(e => e.Id == id);
			return result.FirstOrDefault();
		}

		public async Task UpdateExpressionAsync(ExpressionModel expression)
		{
			await expressions_.ReplaceOneAsync(e => e.Id == expression.Id, expression);
			cache_.Remove(CacheName);
		}

		public async Task UpvoteExpressionAsync(string expressionId, string userId)
		{
			var client = dbConnection_.Client;

			using var session = await client.StartSessionAsync();

			session.StartTransaction();

			try
			{
				var db = client.GetDatabase(dbConnection_.DbName);

				var expressions = db.GetCollection<ExpressionModel>(dbConnection_.ExpressionCollectionName);
				var expression = (await expressions.FindAsync(e => e.Id == expressionId)).First();

				var isUpvoted = expression.UserVotes.Add(userId);
				if (!isUpvoted)
				{
					expression.UserVotes.Remove(userId);
				}

				await expressions.ReplaceOneAsync(e => e.Id == expressionId, expression);

				var users = db.GetCollection<UserModel>(dbConnection_.UserCollectionName);
				var user = await userData_.GetUserAsync(userId);
				if (isUpvoted)
				{
					user.VotedOnExpressions.Add(new BasicExpressionModel(expression));
				}
				else
				{
					var expressionToRemove = user.VotedOnExpressions.Where(e => e.Id == expressionId).First();
					user.VotedOnExpressions.Remove(expressionToRemove);
				}

				await users.ReplaceOneAsync(u => u.Id == userId, user);

				await session.CommitTransactionAsync();

				cache_.Remove(CacheName);
			}
			catch (Exception e)
			{
				// add logging
				await session.AbortTransactionAsync();
				throw;
			}
		}

		public async Task CreateExpressionAsync(ExpressionModel expression)
		{
			var client = dbConnection_.Client;

			using var session = await client.StartSessionAsync();

			session.StartTransaction();

			try
			{
				var db = client.GetDatabase(dbConnection_.DbName);

				var expressions = db.GetCollection<ExpressionModel>(dbConnection_.ExpressionCollectionName);
				await expressions.InsertOneAsync(expression);

				var users = db.GetCollection<UserModel>(dbConnection_.UserCollectionName);
				var user = await userData_.GetUserAsync(expression.Author.Id);
				user.AuthoredExpressions.Add(new BasicExpressionModel(expression));
				await users.ReplaceOneAsync(u => u.Id == user.Id, user);

				await session.CommitTransactionAsync();
			}
			catch (Exception e)
			{
				// add logging
				await session.AbortTransactionAsync();
				throw;
			}
		}
	}
}

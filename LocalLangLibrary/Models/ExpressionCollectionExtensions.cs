using MongoDB.Driver;
using MongoDbAccess;
using MongoDbAccess.DataAccess.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalLangLibrary.Models
{
	[CollectionDefinition]
	public interface IExpressionCollection : IDbCollection<Expression>
	{
		Task<IList<Expression>?> GetPendingAsync();
		Task LikeAsync(Expression expression);
	}

	public partial class MongoExpressionCollection
	{
		public override async Task<IList<Expression>?> GetPendingAsync()
		{
			var result = await GetNotRejectedAsync();
			return result?.Where(e => e.Status == Status.Pending).ToList();
		}

		public override async Task LikeAsync(Expression expression)
		{
			expression.Likes++;
			await UpdateAsync(expression);
		}

		private async Task<IList<Expression>?> GetNotRejectedAsync()
		{
			var result = Cache.Get();
			if (result is null)
			{
				var allExpressions = await Documents.FindAsync(e => e.Status != Status.Rejected);
				result = await allExpressions.ToListAsync();

				Cache.Set(result);
			}

			return result;
		}
	}
}

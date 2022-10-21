using LocalLangLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public interface IExpressionData
	{
		Task CreateExpressionAsync(ExpressionModel expression);
		Task<IList<ExpressionModel>> GetApprovedExpressionsAsync();
		Task<ExpressionModel> GetExpressionAsync(string id);
		Task<IList<ExpressionModel>> GetExpressionsAsync();
		Task<IList<ExpressionModel>> GetUserExpressionsAsync(string userId);
		Task<IList<ExpressionModel>> GetWaitingExpressionsAsync();
		Task UpdateExpressionAsync(ExpressionModel expression);
		Task UpvoteExpressionAsync(string expressionId, string userId);
	}
}
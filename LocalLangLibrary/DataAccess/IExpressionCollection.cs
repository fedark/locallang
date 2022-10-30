using LocalLangLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public interface IExpressionCollection : IDbCollection<Expression>
	{
		Task<IList<Expression>?> GetPendingAsync();
		Task LikeAsync(Expression expression);
	}
}
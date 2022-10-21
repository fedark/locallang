using LocalLangLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalLangLibrary.DataAccess
{
	public interface IStatusData
	{
		Task CreateStatus(StatusModel status);
		Task<IList<StatusModel>> GetStatusesAsync();
	}
}
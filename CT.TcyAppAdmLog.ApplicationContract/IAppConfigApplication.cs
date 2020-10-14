using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.ApplicationContract
{
    public interface IAppConfigApplication
    {
        Task AddAppCacheAsync();
        Task<ApiResult<bool>> AuthAppInfoAsync(int appId, long unixTime, string sign);
        Task<ApiResult<object>> CreateAppAsync(AddAppConfig add);
    }
}
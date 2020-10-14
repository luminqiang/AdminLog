using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ServiceModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.ServiceContract
{
    public interface IAppConfigService
    {
        /// <summary>
        /// 加载所有应用信息至本地缓存
        /// </summary>
        /// <returns></returns>
        Task AddAppCacheAsync();

        /// <summary>
        /// 认证应用访问权限
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="unixTime"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        Task<ServiceInvokeResult<bool>> AuthAppInfoAsync(int appId, long unixTime, string sign);

        /// <summary>
        /// 创建新应用
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        Task<ServiceInvokeResult<AppConfigViewModel>> CreateAppAsync(AddAppConfig add);
    }
}
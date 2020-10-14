using CT.TcyAppAdmLog.ApplicationContract;
using CT.TcyAppAdmLog.Config.Enums;
using CT.TcyAppAdmLog.Framework.Dependency;
using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using CT.TcyAppAdmLog.ServiceContract;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.Application
{
    public class AppConfigApplication : IAppConfigApplication
    {
        public readonly IAppConfigService _appConfigService;

        public AppConfigApplication(IAppConfigService appConfigService)
        {
            _appConfigService = appConfigService;
        }

        public async Task<ApiResult<object>> CreateAppAsync(AddAppConfig add)
        {
            var data = await _appConfigService.CreateAppAsync(add);
            return new ApiResult<object>()
            {
                Code = (int)ApiResultCode.Success,
                Data = data.Result,
                Message = data.Message
            };
        }

        public async Task AddAppCacheAsync()
        {
            await _appConfigService.AddAppCacheAsync();
        }

        public async Task<ApiResult<bool>> AuthAppInfoAsync(int appId, long unixTime, string sign)
        {
            var result = await _appConfigService.AuthAppInfoAsync(appId, unixTime, sign);
            return new ApiResult<bool>()
            {
                Data = result.Result,
                Message = result.Message
            };
        }
    }
}
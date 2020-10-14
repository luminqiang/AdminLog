using CT.TcyAppAdmLog.ApplicationContract;
using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.WebApi.Controllers
{
    [Route("api/appconfig")]
    [ApiController]
    public class AppConfigController : ControllerBase
    {
        private readonly IAppConfigApplication _appConfigApplication;

        public AppConfigController(IAppConfigApplication appConfigApplication)
        {
            _appConfigApplication = appConfigApplication;
        }

        [HttpPost("create")]
        public async Task<ApiResult<object>> ReportAsync([FromBody] AddAppConfig addAppConfig)
        {
            return await _appConfigApplication.CreateAppAsync(addAppConfig);
        }
    }
}
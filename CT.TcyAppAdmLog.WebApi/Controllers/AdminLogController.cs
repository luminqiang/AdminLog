using CT.TcyAppAdmLog.ApplicationContract;
using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using CT.TcyAppAdmLog.WebApi.Filter;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.WebApi.Controllers
{
    [Route("api/adminlog")]
    [ApiController]
    public class AdminLogController : ControllerBase
    {
        private readonly IAdminLogApplication _adminLogApplication;

        public AdminLogController(IAdminLogApplication adminLogApplication)
        {
            _adminLogApplication = adminLogApplication;
        }

        //[AuthAppInfo]
        [HttpPost("report")]       
        public async Task<ApiResult<object>> ReportAsync([FromBody] AdminLogDto adminLog)
        {
            return await _adminLogApplication.ReportAsync(adminLog);
        }

        [AuthAppInfo]
        [HttpGet("query")]
        public async Task<ApiResult<AdminLogViewModel>> QueryAsync([FromQuery] QueryAdminLog query)
        {
            return await _adminLogApplication.QueryAsync(query);
        }        
    }
}
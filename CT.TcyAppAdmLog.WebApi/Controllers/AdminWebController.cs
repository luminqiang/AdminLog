using CT.TcyAppAdmLog.ApplicationContract;
using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.WebApi.Controllers
{
    [Route("api/adminweb")]
    [ApiController]
    public class AdminWebController : ControllerBase
    {
        private readonly IAdminLogApplication _adminLogApplication;

        public AdminWebController(IAdminLogApplication adminLogApplication)
        {
            _adminLogApplication = adminLogApplication;
        }

        [HttpGet("content")]
        public async Task<ApiResult<OperationContentViewModel>> QueryOperationContentAsync([FromQuery] QueryOperationContent query)
        {
            return await _adminLogApplication.QueryOperationContentAsync(query);
        }

        [HttpGet("list")]
        public async Task<ApiResult<PageViewModel<AdminLogViewModel>>> QueryAdminLogListAsync([FromQuery] QueryAdminLogList query)
        {
            return await _adminLogApplication.QueryAdminLogListAsync(query);
        }
    }
}
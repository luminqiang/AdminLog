using CT.TcyAppAdmLog.ApplicationContract;
using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.WebApi.Controllers
{
    [Route("api/operationid")]
    [ApiController]
    public class OperationIdConfigController : ControllerBase
    {
        private readonly IOperationIdConfigApplication _operationIdConfigApplication;

        public OperationIdConfigController(IOperationIdConfigApplication operationIdConfigApplication)
        {
            _operationIdConfigApplication = operationIdConfigApplication;
        }

        [HttpPost("create")]
        public async Task<ApiResult<object>> CreateOperationIdAsync([FromBody] AddOperationIdConfig addAppConfig)
        {
            return await _operationIdConfigApplication.CreateOperationIdAsync(addAppConfig);
        }
    }
}
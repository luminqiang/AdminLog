using CT.TcyAppAdmLog.ApplicationContract;
using CT.TcyAppAdmLog.Config.Enums;
using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using CT.TcyAppAdmLog.ServiceContract;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.Application
{
    public class OperationIdConfigApplication : IOperationIdConfigApplication
    {
        private readonly IOperationIdConfigService _operationIdConfigService;

        public OperationIdConfigApplication(IOperationIdConfigService operationIdConfigService)
        {
            _operationIdConfigService = operationIdConfigService;
        }

        public async Task<ApiResult<object>> CreateOperationIdAsync(AddOperationIdConfig add)
        {
            var data = await _operationIdConfigService.CreateOperationIdAsync(add);
            return new ApiResult<object>()
            {
                Code = data.Result ? (int)ApiResultCode.Success : (int)ApiResultCode.UnknownError,
                Data = null,
                Message = data.Result ? "创建成功" : "创建失败"
            };
        }
    }
}
using CT.TcyAppAdmLog.ApplicationContract;
using CT.TcyAppAdmLog.Config.Enums;
using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using CT.TcyAppAdmLog.ServiceContract;
using System;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.Application
{
    public class AdminLogApplication : IAdminLogApplication
    {
        private readonly IAdminLogService _adminLogService;

        public AdminLogApplication(IAdminLogService adminLogService)
        {
            _adminLogService = adminLogService;
        }

        public async Task<ApiResult<AdminLogViewModel>> QueryAsync(QueryAdminLog query)
        {
            var result = await _adminLogService.QueryAsync(query);
            return new ApiResult<AdminLogViewModel>()
            {
                Code = (int)ApiResultCode.Success,
                Data = result,
                Message = "成功"
            };
        }

        public async Task<ApiResult<PageViewModel<AdminLogViewModel>>> QueryAdminLogListAsync(QueryAdminLogList query)
        {
            var result = await _adminLogService.QueryAdminLogListAsync(query);
            return new ApiResult<PageViewModel<AdminLogViewModel>>()
            {
                Code = (int)ApiResultCode.Success,
                Data = result,
                Message = "成功"
            };
        }

        public async Task<ApiResult<OperationContentViewModel>> QueryOperationContentAsync(QueryOperationContent query)
        {
            var result = await _adminLogService.QueryOperationContentAsync(query);
            return new ApiResult<OperationContentViewModel>()
            {
                Code = result != null ? (int)ApiResultCode.Success : (int)ApiResultCode.ParamError,
                Data = result,
                Message = result != null ? "成功" : "不存在的ID"
            };
        }

        public async Task<ApiResult<object>> ReportAsync(AdminLogDto dto)
        {
            var result = await _adminLogService.ReportAsync(dto);
            return new ApiResult<object>()
            {
                Code = result.Result ? (int)ApiResultCode.Success : (int)ApiResultCode.UnknownError,
                Data = null,
                Message = result.Result ? "成功" : result.Message
            };
        }
    }
}
using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.ApplicationContract
{
    public interface IAdminLogApplication
    {
        Task<ApiResult<object>> ReportAsync(AdminLogDto dto);

        Task<ApiResult<AdminLogViewModel>> QueryAsync(QueryAdminLog query);

        Task<ApiResult<OperationContentViewModel>> QueryOperationContentAsync(QueryOperationContent query);

        Task<ApiResult<PageViewModel<AdminLogViewModel>>> QueryAdminLogListAsync(QueryAdminLogList query);
    }
}
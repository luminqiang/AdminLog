using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ServiceModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.ServiceContract
{
    public interface IAdminLogService
    {
        /// <summary>
        /// 上报日志
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ServiceInvokeResult<bool>> ReportAsync(AdminLogDto dto);

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<AdminLogViewModel> QueryAsync(QueryAdminLog query);

        /// <summary>
        /// 查询日志操作内容
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<OperationContentViewModel> QueryOperationContentAsync(QueryOperationContent query);

        /// <summary>
        /// 查询日志列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<PageViewModel<AdminLogViewModel>> QueryAdminLogListAsync(QueryAdminLogList query);
    }
}
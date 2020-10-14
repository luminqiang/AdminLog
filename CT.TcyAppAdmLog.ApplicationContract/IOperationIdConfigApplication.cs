using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.ApplicationContract
{
    public interface IOperationIdConfigApplication
    {
        /// <summary>
        /// 添加操作ID
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        Task<ApiResult<object>> CreateOperationIdAsync(AddOperationIdConfig add);
    }
}
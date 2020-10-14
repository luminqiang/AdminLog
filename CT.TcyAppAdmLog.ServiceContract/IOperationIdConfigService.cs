using CT.TcyAppAdmLog.Model.DataTransferModels;
using CT.TcyAppAdmLog.Model.ServiceModels;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.ServiceContract
{
    public interface IOperationIdConfigService
    {
        /// <summary>
        /// 添加操作ID
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        Task<ServiceInvokeResult<bool>> CreateOperationIdAsync(AddOperationIdConfig add);
    }
}
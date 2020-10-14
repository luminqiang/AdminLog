using CT.TcyAppAdmLog.Config.Enums;
using CT.TcyAppAdmLog.Model.ServiceModels;
using CT.TcyAppAdmLog.Model.ViewModels;
using CtCommon.Utility;

namespace CT.TcyAppAdmLog.Service
{
    public class BaseServices
    {
        protected ApiResult<T> GetApiResult<T>(ApiResultCode code, T data)
        {
            return new ApiResult<T>() { Code = (int)code, Data = data, Message = code.GetDescription() };
        }

        protected ApiResult<T> GetApiResult<T>(ApiResultCode code, T data, string msg)
        {
            return new ApiResult<T>() { Code = (int)code, Data = data, Message = msg };
        }

        protected ServiceInvokeResult<T> PrintInvokeResult<T>(T result, string message)
        {
            return new ServiceInvokeResult<T>() { Result = result, Message = message };
        }
    }
}
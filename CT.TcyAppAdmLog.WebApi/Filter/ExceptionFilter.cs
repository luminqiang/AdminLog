using CT.TcyAppAdmLog.Config.Enums;
using CT.TcyAppAdmLog.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CT.TcyAppAdmLog.WebApi.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public ExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(nameof(ExceptionFilter));
        }

        public void OnException(ExceptionContext context)
        {
            var controller = context.RouteData.Values["controller"] as string;
            var action = context.RouteData.Values["action"] as string;

            _logger.LogError(context.Exception, $"控制器{controller}-{action}发生异常");

            context.Result = new ApplicationErrorResult(new ApiResult<int>() { Code = (int)ApiResultCode.UnknownError, Message = "系统错误，请重试" });
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;
        }
    }

    public class ApplicationErrorResult : ObjectResult
    {
        public ApplicationErrorResult(object value) : base(value)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
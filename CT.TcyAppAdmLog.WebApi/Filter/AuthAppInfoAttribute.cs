using CT.TcyAppAdmLog.ApplicationContract;
using CT.TcyAppAdmLog.Config.Enums;
using CT.TcyAppAdmLog.Framework;
using CT.TcyAppAdmLog.Model.ViewModels;
using CtCommon.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Net;

namespace CT.TcyAppAdmLog.WebApi.Filter
{
    public class AuthAppInfoAttribute : Attribute, IActionFilter
    {
        private readonly IAppConfigApplication _appConfigApplication;

        public AuthAppInfoAttribute()
        {
            var serviceScope = FrameworkExtensions.ServiceProvider.CreateScope();
            _appConfigApplication = serviceScope.ServiceProvider.GetService<IAppConfigApplication>();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            var appIdExist = headers.TryGetValue("AppId", out StringValues headerOfAppId);
            var unixTimeExist = headers.TryGetValue("UnixTime", out StringValues headerOfUnixTime);
            var signExist = headers.TryGetValue("Sign", out StringValues headerOfSign);

            if (!appIdExist || !unixTimeExist || !signExist)
            {
                context.Result = GetObjectResult("请提供授权信息");
                return;
            }

            int.TryParse(headerOfAppId, out int appId);
            long.TryParse(headerOfUnixTime, out long unixTime);

            if (appId <= 0)
            {
                context.Result = GetObjectResult("AppId错误");
                return;
            }

            var nowUnixTime = DateTime.Now.ToUnixTime(true);
            var isExpired = Math.Abs(nowUnixTime - unixTime) > 3 * 60 * 1000;
            if (isExpired)
            {
                context.Result = GetObjectResult("授权已过期");
                return;
            }

            var result = _appConfigApplication.AuthAppInfoAsync(appId, unixTime, headerOfSign.ToString()).GetAwaiter().GetResult();
            if (!result.Data)
            {
                context.Result = GetObjectResult(result.Message);
                return;
            }
        }

        private ObjectResult GetObjectResult(string message = "未授权")
        {
            var value = new ApiResult<object>()
            {
                Code = (int)ApiResultCode.AccessDenied,
                Data = null,
                Message = message
            };

            return new ObjectResult(value)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
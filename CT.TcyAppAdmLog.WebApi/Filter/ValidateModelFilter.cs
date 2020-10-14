using CT.TcyAppAdmLog.Config.Enums;
using CT.TcyAppAdmLog.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace CT.TcyAppAdmLog.WebApi.Filter
{
    public class ValidateModelFilter : ActionFilterAttribute
    {
        public virtual int ExecOrder
        {
            get;
            set;
        }

        public ValidateModelFilter()
        {
            base.Order = ExecOrder;
        }

        protected virtual void ValidateInput(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var message = string.Empty;
            var keys = context.ModelState.Keys.ToList();
            foreach (var key in keys)
            {
                if (!context.ModelState[key].Errors.Any())
                {
                    continue;
                }

                message += $"参数: {key}, 错误：{string.Join('|', context.ModelState[key].Errors.Select(a => a.ErrorMessage))}; ";
            }

            var apiResult = new ApiResult<object>()
            {
                Code = (int)ApiResultCode.ParamError,
                Data = null,
                Message = message
            };

            context.Result = new ObjectResult(apiResult);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ValidateInput(context);
            base.OnActionExecuting(context);
        }
    }
}
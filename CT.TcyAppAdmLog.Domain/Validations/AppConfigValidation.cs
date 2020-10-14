using CT.TcyAppAdmLog.Domain.Models;
using FluentValidation;

namespace CT.TcyAppAdmLog.Domain.Validations
{
    public class AppConfigValidation : AbstractValidator<AppConfig>
    {
        public AppConfigValidation()
        {
            ValidateAppId();
            ValidateAppCode();
            ValidateAppKey();
            ValidateAppName();
            ValidateCreateUnixTime();
        }

        protected void ValidateAppId()
        {
            RuleFor(c => c.AppId).GreaterThan(0);
        }

        protected void ValidateAppCode()
        {
            RuleFor(c => c.AppCode)
                .NotNull().WithMessage("AppCode不能为Null")
                .NotEmpty().WithMessage("AppCode不能为空");
        }

        protected void ValidateAppKey()
        {
            RuleFor(c => c.AppKey)
                .NotNull().WithMessage("AppKey不能为Null")
                .NotEmpty().WithMessage("AppKey不能为空");
        }

        protected void ValidateAppName()
        {
            RuleFor(c => c.AppKey)
                .NotNull().WithMessage("AppName不能为Null")
                .NotEmpty().WithMessage("AppName不能为空");
        }

        protected void ValidateCreateUnixTime()
        {
            RuleFor(c => c.CreateUnixTime).GreaterThan(0);
        }
    }
}
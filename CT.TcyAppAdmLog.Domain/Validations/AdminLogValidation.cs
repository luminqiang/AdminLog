using CT.TcyAppAdmLog.Domain.Models;
using FluentValidation;

namespace CT.TcyAppAdmLog.Domain.Validations
{
    public abstract class AdminLogValidation : AbstractValidator<AdminLog>
    {
        /// <summary>
        /// 验证应用ID
        /// </summary>
        protected void ValidateAppId()
        {
            RuleFor(c => c.AppId).GreaterThan(0);
        }

        /// <summary>
        /// 验证管理员ID
        /// </summary>
        protected void ValidateAdminId()
        {
            RuleFor(c => c.AdminId).GreaterThan(0);
        }

        /// <summary>
        /// 验证管理员姓名
        /// </summary>
        protected void ValidateAdminName()
        {
            RuleFor(c => c.AdminName)
                .NotNull()
                .NotEmpty().WithMessage("管理员姓名不能为空")
                .Length(1, 25).WithMessage("管理员姓名在1~25个字符之间");
        }
    }
}
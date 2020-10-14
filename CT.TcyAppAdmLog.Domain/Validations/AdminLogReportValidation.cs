using CT.TcyAppAdmLog.Domain.Commands;

namespace CT.TcyAppAdmLog.Domain.Validations
{
    public class AdminLogReportValidation : AdminLogValidation
    {
        public AdminLogReportValidation()
        {
            ValidateAdminId();
            ValidateAdminName();
            ValidateAppId();
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace CT.TcyAppAdmLog.Model.DataTransferModels
{
    public class AddAppConfig
    {
        [Required]
        [StringLength(25)]
        public string AppCode { get; set; }

        [Required]
        [StringLength(25)]
        public string AppName { get; set; }
    }
}
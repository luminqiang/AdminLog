using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CT.TcyAppAdmLog.Model.DataTransferModels
{
    public class AddOperationIdConfig
    {
        [Range(0, int.MaxValue)]
        public int AppId { get; set; }

        public List<OpretationInfo> Opretations { get; set; }
    }

    public class OpretationInfo
    {
        [Range(0, int.MaxValue)]
        public int OperationId { get; set; }

        [Required]
        [MaxLength(50)]
        public string OperationName { get; set; }
    }
}
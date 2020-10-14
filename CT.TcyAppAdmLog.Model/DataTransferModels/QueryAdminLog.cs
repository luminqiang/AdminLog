using System.ComponentModel.DataAnnotations;

namespace CT.TcyAppAdmLog.Model.DataTransferModels
{
    public class QueryAdminLog
    {
        [Range(0, int.MaxValue)]
        public int AppId { get; set; }

        [Range(0, int.MaxValue)]
        public int LinkId { get; set; }

        [Range(0, long.MaxValue)]
        public long OperationId { get; set; }

        public long BeginUnixTime { get; set; }

        public long EndUnixTime { get; set; }
    }
}
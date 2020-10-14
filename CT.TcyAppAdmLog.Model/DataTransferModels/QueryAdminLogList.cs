namespace CT.TcyAppAdmLog.Model.DataTransferModels
{
    public class QueryAdminLogList
    {
        public int AppId { get; set; }

        public long LinkId { get; set; }

        public int OperationId { get; set; }

        public long BeginUnixTime { get; set; }

        public long EndUnixTime { get; set; }
    }
}
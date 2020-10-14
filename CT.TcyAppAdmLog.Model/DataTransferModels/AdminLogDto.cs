namespace CT.TcyAppAdmLog.Model.DataTransferModels
{
    public class AdminLogDto
    {
        public int AppId { get; set; }

        public int AdminId { get; set; }

        public string AdminName { get; set; }

        public string BeforeContent { get; set; }

        public string AfterContent { get; set; }

        public long LinkId { get; set; }

        public int OperationId { get; set; }

        public string OperationIP { get; set; }

        public string OperationRemark { get; set; }

        public long CreateUnixTime { get; set; }
    }
}
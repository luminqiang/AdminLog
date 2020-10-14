namespace CT.TcyAppAdmLog.Model.ViewModels
{
    public class AdminLogViewModel
    {
        public int AppId { get; set; }

        public int AdminId { get; set; }

        public string AdminName { get; set; }

        public int LinkId { get; set; }

        public int OperationId { get; set; }

        public string OperationIP { get; set; }

        public string OperationRemark { get; set; }

        public long CreateUnixTime { get; set; }
    }
}
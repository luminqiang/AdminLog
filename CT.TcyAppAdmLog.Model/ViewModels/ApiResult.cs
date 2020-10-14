namespace CT.TcyAppAdmLog.Model.ViewModels
{
    public class ApiResult<T>
    {
        /// <summary>
        /// 返回代号
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; } = "成功";
    }
}
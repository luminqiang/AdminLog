namespace CT.TcyAppAdmLog.Model.ServiceModels
{
    public class ServiceInvokeResult<T>
    {
        public T Result { get; set; }

        public string Message { get; set; }
    }
}
using CT.TcyAppAdmLog.Domain.Core.Model;
using SqlSugar;

namespace CT.TcyAppAdmLog.Domain.Models
{
    [SugarTable("operationidconfig")]
    public class OperationIdConfig : Entity
    {
        public OperationIdConfig()
        {
        }

        public OperationIdConfig(int appId, int operationId, string operationName, long createUnixTime)
        {
            AppId = appId;
            OperationId = operationId;
            OperationName = operationName;
            CreateUnixTime = createUnixTime;
        }

        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppId { get; private set; }

        /// <summary>
        /// 操作ID
        /// </summary>
        public int OperationId { get; private set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        public string OperationName { get; private set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public long CreateUnixTime { get; private set; }
    }
}
using CT.TcyAppAdmLog.Domain.Core;
using CT.TcyAppAdmLog.Domain.Core.Model;
using SqlSugar;

namespace CT.TcyAppAdmLog.Domain.Models
{
    [SugarTable("adminlog")]
    public class AdminLog : Entity, IAggregateRoot
    {
        public AdminLog()
        {
        }

        public AdminLog(int appId, string beforeContent, string afterContent, long linkId, int operationId,
            string operationIP, string operationRemark, long createUnixTime, Administrator administrator)
        {
            AppId = appId;
            BeforeContent = beforeContent;
            AfterContent = afterContent;
            LinkId = linkId;
            OperationId = operationId;
            OperationIP = operationIP;
            OperationRemark = operationRemark;
            CreateUnixTime = createUnixTime;
            AdminId = administrator.AdminId;
            AdminName = administrator.AdminName;
        }

        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppId { get; private set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminId { get; private set; }

        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AdminName { get; private set; }

        /// <summary>
        /// 操作前内容
        /// </summary>
        public string BeforeContent { get; private set; }

        /// <summary>
        /// 操作后内容
        /// </summary>
        public string AfterContent { get; private set; }

        /// <summary>
        /// 关联ID
        /// </summary>
        public long LinkId { get; private set; }

        /// <summary>
        /// 操作ID
        /// </summary>
        public int OperationId { get; private set; }

        /// <summary>
        /// 操作IP
        /// </summary>
        public string OperationIP { get; private set; }

        /// <summary>
        /// 操作备注
        /// </summary>
        public string OperationRemark { get; private set; }

        /// <summary>
        /// 操作记录入库时间
        /// </summary>
        public long CreateUnixTime { get; private set; }
    }
}
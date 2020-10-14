using CT.TcyAppAdmLog.Domain.Core.Model;
using CT.TcyAppAdmLog.Domain.Validations;
using SqlSugar;

namespace CT.TcyAppAdmLog.Domain.Models
{
    [SugarTable("appconfig")]
    public class AppConfig : Entity
    {
        public AppConfig()
        {

        }

        public AppConfig(int appId, string appCode, string appKey, string appName, long createUnixTime)
        {
            AppId = appId;
            AppCode = appCode;
            AppKey = appKey;
            AppName = appName;
            CreateUnixTime = createUnixTime;
        }

        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppId { get; private set; }

        /// <summary>
        /// 应用Code 对应服务管理中的服务代号
        /// </summary>
        public string AppCode { get; private set; }

        /// <summary>
        /// 应用访问秘钥
        /// </summary>
        public string AppKey { get; private set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; private set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public long CreateUnixTime { get; private set; }       
    }
}
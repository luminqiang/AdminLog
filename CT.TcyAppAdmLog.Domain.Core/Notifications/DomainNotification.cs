using CT.TcyAppAdmLog.Domain.Core.Events;
using System;

namespace CT.TcyAppAdmLog.Domain.Core.Notifications
{
    /// <summary>
    /// 领域通知模型
    /// </summary>
    public class DomainNotification : Event
    {
        /// <summary>
        /// 通知标识
        /// </summary>
        public Guid DomainNotificationId { get; private set; }

        /// <summary>
        /// 通知信息 键
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 通知信息 值
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// 版本信息
        /// </summary>
        public int Version { get; private set; }

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}
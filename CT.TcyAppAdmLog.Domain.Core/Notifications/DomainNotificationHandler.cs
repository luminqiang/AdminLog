using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.Domain.Core.Notifications
{
    /// <summary>
    /// 领域通知处理程序，把所有的通知信息放到事件总线中
    /// 继承 INotificationHandler<T>
    /// </summary>
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        public readonly ILogger _logger;

        /// <summary>
        /// 通知信息列表
        /// </summary>
        private List<DomainNotification> _notifications;

        /// <summary>
        /// 每次访问该处理程序的时候，实例化一个空集合
        /// </summary>
        public DomainNotificationHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(nameof(DomainNotificationHandler));
            _notifications = new List<DomainNotification>();
        }

        /// <summary>
        /// 处理方法，把全部的通知信息，添加到内存里
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            _logger.LogWarning($"消息类型: {message.MessageType} 消息错误: {JsonConvert.SerializeObject(message.Value)}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取当前生命周期内的全部通知信息
        /// </summary>
        /// <returns></returns>
        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        /// <summary>
        /// 判断在当前总线对象周期中，是否存在通知信息
        /// </summary>
        /// <returns></returns>
        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        /// <summary>
        /// 手动回收（清空通知）
        /// </summary>
        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}
using MediatR;
using System;

namespace CT.TcyAppAdmLog.Domain.Core.Events
{
    /// <summary>
    /// 抽象类Message，用来获取事件执行过程中的类名
    /// 然后并且添加聚合根
    /// </summary>
    public abstract class Message : IRequest
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
            AggregateId = new Guid();
        }
    }
}
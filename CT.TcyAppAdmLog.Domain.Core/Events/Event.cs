using FluentValidation.Results;
using MediatR;
using System;

namespace CT.TcyAppAdmLog.Domain.Core.Events
{
    /// <summary>
    /// 事件模型 抽象基类
    /// IRequest 单播 请求响应模式
    /// INotification 多播模式
    /// </summary>
    public abstract class Event : Message, INotification
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// 验证结果
        /// </summary>
        public ValidationResult ValidationResult { get; set; }

        /// <summary>
        /// 构造函数初始化事件状态
        /// </summary>
        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
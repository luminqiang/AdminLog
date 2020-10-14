using CT.TcyAppAdmLog.Domain.Core.Events;
using FluentValidation.Results;
using System;

namespace CT.TcyAppAdmLog.Domain.Core.Commands
{
    /// <summary>
    /// 抽象命令基类
    /// 继承IRequest,请求响应模式，单播
    /// </summary>
    public abstract class Command : Message
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// 验证结果
        /// </summary>
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// 定义抽象方法，是否有效
        /// </summary>
        /// <returns></returns>
        public abstract bool IsValid();
    }
}
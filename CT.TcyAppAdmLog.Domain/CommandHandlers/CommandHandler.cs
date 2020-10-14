using CT.TcyAppAdmLog.Domain.Core.Bus;
using CT.TcyAppAdmLog.Domain.Core.Commands;
using CT.TcyAppAdmLog.Domain.Core.Notifications;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CT.TcyAppAdmLog.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IMediatorHandler _bus;

        public CommandHandler(IMediatorHandler bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// 收集领域命令中的验证错误信息
        /// </summary>
        /// <param name="message"></param>
        protected void NotifyValidationErrors(Command message)
        {
            List<string> errorInfo = new List<string>();
            foreach (var error in message.ValidationResult.Errors)
            {
                errorInfo.Add(error.ErrorMessage);             
            }

            //将错误信息提交到事件总线，派发出去
            var errorStr = JsonConvert.SerializeObject(errorInfo);
            _bus.RaiseEvent(new DomainNotification(message.MessageType, errorStr));
        }
    }
}
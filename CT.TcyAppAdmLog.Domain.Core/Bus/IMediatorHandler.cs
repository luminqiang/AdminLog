using CT.TcyAppAdmLog.Domain.Core.Commands;
using CT.TcyAppAdmLog.Domain.Core.Events;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.Domain.Core.Bus
{
    /// <summary>
    /// 中介处理接口
    /// 可以定义多个处理程序
    /// </summary>
    public interface IMediatorHandler
    {
        /// <summary>
        /// 发送命令，将命令模型发布到中介者模块
        /// </summary>
        /// <typeparam name="T"> 泛型 </typeparam>
        /// <param name="command"> 命令模型 </param>
        /// <returns></returns>
        Task SendCommand<T>(T command) where T : Command;

        /// <summary>
        /// 引发事件，通过总线，发布事件
        /// </summary>
        /// <typeparam name="T"> 泛型 继承 Event：INotification</typeparam>
        /// <param name="event"> 事件模型 </param>
        /// <returns></returns>
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
using CT.TcyAppAdmLog.Domain.Core.Bus;
using CT.TcyAppAdmLog.Domain.Core.Commands;
using CT.TcyAppAdmLog.Domain.Core.Events;
using MediatR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace CT.TcyAppAdmLog.Domain.Bus
{
    /// <summary>
    /// 一个密封类，实现我们的中介内存总线
    /// </summary>
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, object> _requestHandlers = new ConcurrentDictionary<Type, object>();

        public InMemoryBus(IMediator mediator, ServiceFactory serviceFactory)
        {
            _mediator = mediator;
            _serviceFactory = serviceFactory;
        }

        /// <summary>
        /// 实现我们在IMediatorHandler中定义的接口
        /// 没有返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        /// <summary>
        /// 引发事件的实现方法
        /// </summary>
        /// <typeparam name="T">泛型 继承 Event：INotification</typeparam>
        /// <param name="event">事件模型</param>
        /// <returns></returns>
        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }
    }
}
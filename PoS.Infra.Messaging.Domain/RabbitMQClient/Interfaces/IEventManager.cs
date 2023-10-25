using System;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Settings;

namespace PoS.Infra.Messaging.Domain.RabbitMQClient.Interfaces
{
    /// <summary>
    /// Specifies the default model for any event manager.
    /// </summary>
    public interface IEventManager
    {
        /// <summary>
        /// Indicates the working exchange of the event.
        /// </summary>
        public String WorkingArea { get; init; }

        /// <summary>
        /// Indicates the working queue of the event.
        /// </summary>
        public String WorkingQueue { get; init; }

        /// <summary>
        /// Specifies the event dispatching configuration.
        /// </summary>
        public DispatchType DispatchType { get; init; }

        /// <summary>
        /// Indicates the routing key used for binding the event's queue and its corresponding working exchange.
        /// </summary>
        public String ExchangeQueueBindingKey { get; init; }
    }
}
using System;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Settings;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Interfaces;

namespace PoS.Infra.Messaging.Domain.RabbitMQClient.Models
{
    /// <summary>
    /// Holds any event instrinsic feature such as the event queue.
    /// </summary>
    public class EventManager : IEventManager
    {
        /// <inheritdoc/>
        public String WorkingArea { get; init; }

        /// <inheritdoc/>
        public String WorkingQueue { get; init; }

        /// <inheritdoc/>
        public String ExchangeQueueBindingKey { get; init; }

        /// <inheritdoc/>
        public DispatchType DispatchType { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="EventManager"/> with the specified arguments.
        /// </summary>
        /// <param name="workingExchange">Indicates the working exchange of the event.</param>
        /// <param name="workingExchange">Indicates the working queue of the event.</param>
        public EventManager(String workingExchange, String workingQueue, String exchangeQueueBindingKey, DispatchType dispatchType)
        {
            this.WorkingArea = workingExchange;
            this.WorkingQueue = workingQueue;
            this.ExchangeQueueBindingKey = exchangeQueueBindingKey;
            this.DispatchType = dispatchType;
        }
    }
}

using System;
using PoS.Infra.Messaging.RabbitMQClient.Models;
using PoS.Infra.Messaging.RabbitMQClient.Interfaces;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Interfaces;

namespace PoS.Infra.Messaging.RabbitMQClient.SendingServices
{
    /// <summary>
    /// <see cref="RabbitMQInterceptionService"/> provides access for enqueueing, consuming and general-purpose functionalities for working with RabbitMQ.
    /// </summary>
    public class RabbitMQInterceptionService : IRabbitMQInterceptionService
    {
        private IRabbitMQBroker RabbitMQBroker { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="RabbitMQInterceptionService"/>.
        /// </summary>
        public RabbitMQInterceptionService()
        {
            IBrokerConnectionArgs brokerConnectionArgs = new BrokerConnectionArgs(connectionUri: new Uri("amqp://localhost"));
            this.RabbitMQBroker = new RabbitMQBroker(brokerConnectionArgs);
        }

        /// <inheritdoc/>
        public Boolean Enqueue(IEventManager eventManager, Object content)
        {
            return this.RabbitMQBroker.PublishMessage(exchange: eventManager.WorkingArea, queue: eventManager.WorkingQueue,
                queueExchangeBindingKey: eventManager.ExchangeQueueBindingKey, exchangeType: eventManager.DispatchType, message: content).ResultValue;
        }
    }
}
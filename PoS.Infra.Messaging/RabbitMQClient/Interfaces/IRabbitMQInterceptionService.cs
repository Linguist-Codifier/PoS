using System;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Interfaces;

namespace PoS.Infra.Messaging.RabbitMQClient.Interfaces
{
    /// <summary>
    /// Specifies the default behavior and functionalies that any RabbitMQ event interceptor must implement.
    /// </summary>
    public interface IRabbitMQInterceptionService
    {
        /// <summary>
        /// Enqueues a message with the specified arguments.
        /// </summary>
        /// <param name="exchange">The exchange from where the specified queue belongs to.</param>
        /// <param name="queue">The queue to where the message content will be sent.</param>
        /// <param name="content">The message content.</param>
        /// <returns><see langword="true"/> whether no operational exception have not occured; otherwise <see langword="false"/>.</returns>
        public Boolean Enqueue(IEventManager eventManager, Object content);
    }
}
using System;
using PoS.Order.Domain.Wrappers;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Settings;

namespace PoS.Infra.Messaging.RabbitMQClient.Interfaces
{
    /// <summary>
    /// Specifies the default RabbitMQ Broker's functions.
    /// </summary>
    public interface IRabbitMQBroker
    {
        /// <summary>
        /// Publishes a message into the RabbitMQ Broker with the specified arguments.
        /// </summary>
        /// <param name="exchange">The exchange responsible for the message to be sent.</param>
        /// <param name="queue">The queue to where to message will be sent.</param>
        /// <param name="queueExchangeBindingKey">The binding key between the provided exchange name and the queue to where the message will be sent.</param>
        /// <param name="exchangeType">The type of message dispatching.</param>
        /// <param name="message">The message content.</param>
        /// <returns><see cref="WrapperFor{WrappedType}"/> that indicates whether the message had success during sending.</returns>
        public WrapperFor<Boolean> PublishMessage(String exchange, String queue, String queueExchangeBindingKey, DispatchType exchangeType, Object message);

        /// <summary>
        /// Indicates whether the connection with the RabbitMQ Broker Server had success.
        /// </summary>
        /// <returns><see langword="true"/> whether the connection was stabilished with success; otherwise <see langword="false"/>.</returns>
        Boolean HasConnectionUnexpectedBehaviorException();

        /// <summary>
        /// Gets the connection operation exception.
        /// </summary>
        /// <returns>An operational <see cref="Exception"/> when a connection with the RabbitMQ Broker Server did not succeeded; or <see langword="null"/> when no error happened.</returns>
        Exception? GetConnectionUnexpectedBehaviorException();

        /// <summary>
        /// Indicates whether the connection channel with the <see cref="IRabbitMQBroker"/> service implementation is open or not.
        /// </summary>
        /// <returns><see langword="true"></see> whether the connection is open; otherwise <see langword="false"/>.</returns>
        Boolean IsConnectionOpen();
    }
}
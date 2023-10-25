using System;
using PoS.Order.Domain.Interfaces.Wrapping;

namespace PoS.Infra.Messaging.RabbitMQClient.Interfaces
{
    /// <summary>
    /// Specifies the default bahavior and properties that any event handler must implement.
    /// </summary>
    public interface IEventDealer
    {
        /// <summary>
        /// Processes the <typeparamref name="T"/> content wrapped by the incomming <see cref="IEvent{EventType}"/> where <see langword="EventType"/> is <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of content wrapped by the event.</typeparam>
        /// <param name="event">The event to be processed.</param>
        /// <returns><see langword="true"/> whther the process operation had success by not throwing none runtime exception over the MQ broker engine level; otherwise <see langword="false"/>.</returns>
        IWrapperFor<Boolean> Process<T>(IEvent<T> @event) where T : class;
    }
}
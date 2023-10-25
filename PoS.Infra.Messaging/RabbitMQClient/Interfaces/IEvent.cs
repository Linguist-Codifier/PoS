using System;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Interfaces;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Settings;

namespace PoS.Infra.Messaging.RabbitMQClient.Interfaces
{
    /// <summary>
    /// Specifies the default model for any event.
    /// </summary>
    /// <typeparam name="EventType">The event scoped-type.</typeparam>
    public interface IEvent<EventType> : IEventBase where EventType : class
    {
        /// <summary>
        /// The type of the event hoster.
        /// </summary>
        public abstract Type Type { get; init; }

        /// <summary>
        /// The type of the event held by its corresponding event hoster.
        /// </summary>
        public abstract Type EventUnderlyingType { get; init; }

        /// <summary>
        /// The event owner.
        /// </summary>
        public abstract EventOwnership Owner { get; init; }

        /// <summary>
        /// The event manager.
        /// </summary>
        public abstract IEventManager Manager { get; init; }

        /// <summary>
        /// The event Content.
        /// </summary>
        public abstract EventType Content { get; init; }
    }
}
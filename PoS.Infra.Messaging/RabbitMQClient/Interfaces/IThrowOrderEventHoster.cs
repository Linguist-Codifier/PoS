using System;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Interfaces;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Settings;

namespace PoS.Infra.Messaging.RabbitMQClient.Interfaces
{
    public interface IThrowOrderEventHoster<EventScope> : IEvent<EventScope> where EventScope : class
    {
        /// <summary>
        /// The type of the event hoster.
        /// </summary>
        public new Type Type { get; init; }

        /// <summary>
        /// The type of the event held by its corresponding event hoster.
        /// </summary>
        public new Type EventUnderlyingType { get; init; }

        /// <summary>
        /// The event owner.
        /// </summary>
        public new EventOwnership Owner { get; init; }

        /// <summary>
        /// The event manager.
        /// </summary>
        public new IEventManager Manager { get; init; }

        /// <summary>
        /// The event Content.
        /// </summary>
        public new EventScope Content { get; init; }
    }
}
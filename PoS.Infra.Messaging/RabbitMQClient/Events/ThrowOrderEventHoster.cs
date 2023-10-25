using System;
using PoS.Order.Domain.Models;
using PoS.Infra.Messaging.RabbitMQClient.Interfaces;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Models;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Interfaces;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Settings;

namespace PoS.Infra.Messaging.RabbitMQClient.Events
{
    /// <summary>
    /// Represents an <typeparamref name="EventScopeType"/> order-request event.
    /// </summary>
    /// <typeparam name="EventScopeType"></typeparam>
    public class ThrowOrderEventHoster<EventScopeType> : IThrowOrderEventHoster<EventScopeType> where EventScopeType : OrderType
    {
        /// <inheritdoc/>
        public DateTime CreationDateTime { get; init; }

        /// <inheritdoc/>
        public Type Type { get; init; }

        /// <inheritdoc/>
        public Type EventUnderlyingType { get; init; }

        /// <inheritdoc/>
        public EventOwnership Owner { get; init; }

        /// <inheritdoc/>
        public IEventManager Manager { get; init; }

        /// <inheritdoc/>
        public EventScopeType Content { get; init; }

        /// <summary>
        /// Intitializes a new instance of <see cref="ThrowOrderEventHoster{EventType}"/> with the specified arguments.
        /// </summary>
        /// <param name="eventOwner">The event owner.</param>
        /// <param name="eventManager">The event manager.</param>
        /// <param name="eventContent">The event content itself.</param>
        public ThrowOrderEventHoster(EventOwnership eventOwner, IEventManager eventManager, EventScopeType eventContent)
        {
            this.CreationDateTime = DateTime.Now;
            this.Type = this.GetType();
            this.EventUnderlyingType = typeof(EventScopeType);
            this.Owner = eventOwner;
            this.Manager = eventManager;
            this.Content = eventContent;
        }
    }
}
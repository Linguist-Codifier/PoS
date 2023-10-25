using System;
using PoS.Order.Domain.Wrappers;
using PoS.Order.Domain.Interfaces.Wrapping;
using PoS.Infra.Messaging.RabbitMQClient.Interfaces;

namespace PoS.Infra.Messaging.RabbitMQClient.Handlers
{
    /// <summary>
    /// Handles events considering the specified event type during processing call.
    /// </summary>
    public class EventDealer : IEventDealer
    {
        private readonly IRabbitMQInterceptionService rabbitMQInterceptionSerice;

        /// <summary>
        /// Initializes a new instance of <see cref="EventDealer"/> with the specified arguments.
        /// </summary>
        /// <param name="rabbitMQInterceptionService">Any <see cref="IRabbitMQInterceptionService"></see> service implementation instance.</param>
        public EventDealer(IRabbitMQInterceptionService rabbitMQInterceptionService) => this.rabbitMQInterceptionSerice = rabbitMQInterceptionService;

        /// <inheritdoc/>
        public IWrapperFor<Boolean> Process<T>(IEvent<T> @event) where T : class
        {
            return new WrapperFor<Boolean>(this.rabbitMQInterceptionSerice.Enqueue(eventManager: @event.Manager, content: @event.Content));
        }
    }
}
using System;

namespace PoS.Infra.Messaging.RabbitMQClient.Interfaces
{
    /// <summary>
    /// Specifies the inner default behavior and properties model for any event hoster model.
    /// </summary>
    public interface IEventBase
    {
        /// <summary>
        /// The date and time details about the event.
        /// </summary>
        public abstract DateTime CreationDateTime { get; init; }
    }
}
namespace PoS.Infra.Messaging.Domain.RabbitMQClient.Settings
{
    /// <summary>
    /// Specifies any event queue handler's exchange.
    /// </summary>
    public enum EventOwnership : System.UInt32
    {
        /// <summary>
        /// Indicates that the event queue attached to the underlying event belongs to the <see cref="Kitchen"/> exchange.
        /// </summary>
        Kitchen = 0
    }
}

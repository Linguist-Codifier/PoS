namespace PoS.Infra.Messaging.Domain.RabbitMQClient.Settings
{
    /// <summary>
    /// Specifies the event dispatching configuration.
    /// </summary>
    public enum DispatchType
    {
        /// <summary>
        /// The Direct AMQP exchange type.
        /// </summary>
        Direct = 0,

        /// <summary>
        /// The Fanout AMQP exchange type.
        /// </summary>
        Fanout,

        /// <summary>
        /// The Headers AMQP exchange type.
        /// </summary>
        Headers,
        
        /// <summary>
        /// The Topic AMQP exchange type.
        /// </summary>
        Topic
    }
}
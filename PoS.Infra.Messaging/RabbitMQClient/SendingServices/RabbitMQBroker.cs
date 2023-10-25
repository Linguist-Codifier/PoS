using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RabbitMQ.Client;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Settings;
using PoS.Infra.Messaging.RabbitMQClient.Interfaces;
using PoS.Order.Domain.Wrappers;
using PoS.Order.Domain.Interfaces.Wrapping;

namespace PoS.Infra.Messaging.RabbitMQClient.SendingServices
{
    /// <summary>
    /// Exposes the RabbitMQ Broker's functions.
    /// </summary>
    public class RabbitMQBroker : IRabbitMQBroker
    {
        private readonly IConnectionFactory? connectionFactory;

        private readonly IConnection? serverConnection;

        private readonly IModel? connectionChannel;

        private Dictionary<String, Exception>? runtimeExceptions;

        /// <summary>
        /// Initializes a new instance of <see cref="RabbitMQBroker"/> with the specified argument.
        /// </summary>
        /// <param name="connectionArgs">The broker connection settings.</param>
        public RabbitMQBroker(IBrokerConnectionArgs connectionArgs)
        {
            try
            {
                this.connectionFactory = new ConnectionFactory()
                { 
                    Uri = connectionArgs.ConnectionUri ?? new Uri("amqp://localhost:5672")
                };

                this.serverConnection = this.connectionFactory.CreateConnection(); this.connectionChannel = this.serverConnection.CreateModel();
            }
            catch (Exception messageBrokerConnectionUnexpectedBehaviorException)
            {
                this.runtimeExceptions = new()
                {
                    { "Unexpected_Connection_Behavior_Exception", messageBrokerConnectionUnexpectedBehaviorException}
                };
            }
        }

        private IWrapperFor<Byte[]?> EncodeObject(Object artifact, EncodingStrategy encodingStrategy)
        {
            if (encodingStrategy.Equals(EncodingStrategy.UTF8))
                return new WrapperFor<Byte[]?>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize<Object>(artifact)));

            else
            {
                if (this.runtimeExceptions is null)
                {
                    this.runtimeExceptions = new()
                    {
                        {
                            "Unimplemented_Encoding_Strategy", new Exception("Encoding strategy implementation is not set.", new Exception($"Could not find any {nameof(EncodingStrategy)} implementation for [{encodingStrategy}]."))
                        }
                    };
                }
                else
                {
                    this.runtimeExceptions.Add("Unimplemented_Encoding_Strategy", new Exception("Encoding strategy implementation is not set.", new Exception($"Could not find any {nameof(EncodingStrategy)} implementation for [{encodingStrategy}].")));
                }
            }
            return new WrapperFor<Byte[]?>(null);
        }

        [SuppressMessage(category: "Arquitecture", checkId: "CA1822")]
        private Instance TryEnsureInstanceFor<Instance>(Object? instance)
        {
            if (instance is not null and Instance)
                return (Instance)instance;

            throw new InvalidCastException($"Failed on trying to convert [{(instance is null ? "null" : instance.GetType().Name)}] into [{typeof(Instance).Name}].");
        }

        [SuppressMessage(category: "Arquitecture", checkId: "CA1822")]
        private String? TryParseExchangeType(DispatchType dispatchType) => Enum.GetName(dispatchType.GetType(), dispatchType)?.ToLower();

        /// <inheritdoc/>
        public Boolean HasConnectionUnexpectedBehaviorException() => this.runtimeExceptions?.ContainsKey("Unexpected_Connection_Behavior_Exception") ?? false;

        /// <inheritdoc/>
        public Boolean IsConnectionOpen() => this.connectionChannel?.IsOpen ?? false;

        /// <inheritdoc/>
        public Exception? GetConnectionUnexpectedBehaviorException() => this.runtimeExceptions?["Unexpected_Connection_Behavior_Exception"];

        /// <inheritdoc/>
        public WrapperFor<Boolean> PublishMessage(String exchange, String queue, String queueExchangeBindingKey, DispatchType exchangeType, Object message)
        {
            try
            {
                if (!this.HasConnectionUnexpectedBehaviorException() && this.IsConnectionOpen())
                {
                    IWrapperFor<Byte[]?> messageContentEncodingWrapper = this.EncodeObject(message, EncodingStrategy.UTF8);

                    if (messageContentEncodingWrapper.HasWrappedResultValue())
                    {
                        IModel brokerCommunicationChannel = this.TryEnsureInstanceFor<IModel>(this.connectionChannel);

                        brokerCommunicationChannel.ExchangeDeclare(exchange, this.TryParseExchangeType(exchangeType) ?? "Default", true, false);

                        brokerCommunicationChannel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, null);

                        brokerCommunicationChannel.QueueBind(queue: queue, exchange: exchange, routingKey: queueExchangeBindingKey);

                        brokerCommunicationChannel.BasicPublish(exchange: exchange, routingKey: queueExchangeBindingKey, body: this.TryEnsureInstanceFor<Byte[]>(messageContentEncodingWrapper.ResultValue));

                        return new WrapperFor<Boolean>(true);
                    }
                }
                return new WrapperFor<Boolean>(false);
            }
            catch
            {
                return new WrapperFor<Boolean>(false);
            }
        }
    }
}
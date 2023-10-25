using System;

namespace PoS.Infra.Messaging.RabbitMQClient.Interfaces
{
    /// <summary>
    /// Specifies the default broker connections settings.
    /// </summary>
    public interface IBrokerConnectionArgs
    {
        /// <summary>
        /// The connection URI.
        /// </summary>
        public Uri ConnectionUri { get; init; }

        /// <summary>
        /// The connection host.
        /// </summary>
        public String? HostName { get; init; }

        /// <summary>
        /// The connection virtual host.
        /// </summary>
        public String? VirtualHost { get; init; }

        /// <summary>
        /// The connection port.
        /// </summary>
        public Int32? Port { get; init; }

        /// <summary>
        /// The connection user name.
        /// </summary>
        public String? UserName { get; init; }

        /// <summary>
        /// The connection user password.
        /// </summary>
        public String? UserPassword { get; init; }
    }
}
using System;
using PoS.Infra.Messaging.RabbitMQClient.Interfaces;

namespace PoS.Infra.Messaging.RabbitMQClient.Models
{
    /// <summary>
    /// Specifies theunderlying broker's connection settings.
    /// </summary>
    public readonly struct BrokerConnectionArgs : IBrokerConnectionArgs
    {
        /// <inheritdoc/>
        public Uri ConnectionUri { get; init; }

        /// <inheritdoc/>
        public String? HostName { get; init; }

        /// <inheritdoc/>
        public String? VirtualHost { get; init; }

        /// <inheritdoc/>
        public Int32? Port { get; init; }

        /// <inheritdoc/>
        public String? UserName { get; init; }

        /// <inheritdoc/>
        public String? UserPassword { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="BrokerConnectionArgs"/> with the specified argument.
        /// </summary>
        /// <param name="hostName">The broker host name.</param>
        public BrokerConnectionArgs(Uri connectionUri, String? hostName = null, String? virtualHost = null, Int32? port = null, String? userName = null, String? userPassword = null)
        {
            this.ConnectionUri = connectionUri;
            this.HostName = hostName;
            this.VirtualHost = virtualHost;
            this.Port = port;
            this.UserName = userName;
            this.UserPassword = userPassword;
        }
    }
}
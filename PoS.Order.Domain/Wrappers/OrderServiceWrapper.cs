using System;

namespace PoS.Order.Domain.Wrappers
{
    /// <summary>
    /// Wrappes <typeparamref name="ServiceInstanceType"/> services.
    /// </summary>
    /// <typeparam name="ServiceInstanceType"></typeparam>
    public class OrderServiceWrapper<ServiceInstanceType>
    {
        private Object? ServiceInstanceReference { get; set; }

        /// <summary>
        /// The <typeparamref name="ServiceInstanceType"/> service instance.
        /// </summary>
        public ServiceInstanceType Instance
        {
            get
            {
                if (this.ServiceInstanceReference is not null)
                    return (ServiceInstanceType)this.ServiceInstanceReference; throw new NullReferenceException($"Object reference not set to an instance of {typeof(ServiceInstanceType).Name}.");
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="OrderServiceWrapper{ServiceInstanceType}"/> with the specified service object reference.
        /// </summary>
        /// <param name="serviceInstance">The service instance.</param>
        public OrderServiceWrapper(Object serviceInstance)
        {
            if(serviceInstance is ServiceInstanceType)
                this.ServiceInstanceReference = serviceInstance;
            else
                throw new ArgumentOutOfRangeException(message: "Instance type is out of scope.", new Exception(message: $"{nameof(serviceInstance)} is not {typeof(ServiceInstanceType).Name}."));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="OrderServiceWrapper{ServiceInstanceType}"/> with no service object reference.
        /// </summary>
        public OrderServiceWrapper() { }

        /// <summary>
        /// Determines whether this current wrapper holds any service instance or not.
        /// </summary>
        /// <returns><see langword="true"></see> whether there is a service object reference; otherwise <see langword="false"/>.</returns>
        public Boolean HasServiceInstance() => this.Instance is not null;
    }
}

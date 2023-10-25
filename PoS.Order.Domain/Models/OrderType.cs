using System;
using PoS.Order.Domain.Interfaces.Models;

namespace PoS.Order.Domain.Models
{
    /// <summary>
    /// Specifies any order-request's model.
    /// </summary>
    public abstract class OrderType : IOrder
    {
        /// <inheritdoc/>
        public abstract Guid Id { get; set; }
    }
}
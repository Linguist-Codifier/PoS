using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PoS.Order.Domain.Interfaces.Repository;
using PoS.Order.Domain.Wrappers;

namespace PoS.Order.Domain.Interfaces.Services
{
    /// <summary>
    /// Provides abstract data-layer access to the <typeparamref name="OrderType"/> table-repository.
    /// </summary>
    /// <typeparam name="OrderType">The table-repository.</typeparam>
    public interface IOrderRequestTrackingService<OrderType> where OrderType : Domain.Models.OrderType
    {
        /// <summary>
        /// Gets asyncronously an order-request by id.
        /// </summary>
        /// <param name="id">The oroder-request id.</param>
        /// <returns>A <see cref="IDbOperation{TEntity}"/> holding the operation status, and the order-request query result.</returns>
        Task<IDbOperation<OrderType>> GetOrderRequestAsync(Guid id);

        /// <summary>
        ///Gets asyncronously all order-request.
        /// </summary>
        /// <returns>A <see cref="IDbOperation{TEntity}"/> holding the operation status, and the order-request query result.</returns>
        Task<IDbOperation<IEnumerable<OrderType>>> GetOrderRequestsAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<IDbOperation<WrapperFor<Boolean>>> RegisterOrderRequestAsync(OrderType orderRequest);
    }
}
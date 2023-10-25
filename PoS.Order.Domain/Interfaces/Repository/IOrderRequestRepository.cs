using PoS.Infra.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using PoS.Order.Domain.Filters.Attributes;
using PoS.Order.Domain.Models;

namespace PoS.Order.Domain.Interfaces.Repository
{
    /// <summary>
    /// Specifies the entities for any <see cref="IOrderRequestRepository"/> implementation.
    /// </summary>
    public interface IOrderRequestRepository : IPoSRepository
    {
        /// <summary>
        /// Gets the orders-requests entity.
        /// </summary>
        DbSet<DefaultOrderRequest> Orders { get; set; }
    }
}
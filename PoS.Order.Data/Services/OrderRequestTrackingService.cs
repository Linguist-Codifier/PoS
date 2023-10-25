using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PoS.Order.Domain.Interfaces.Services;
using PoS.Order.Domain.Interfaces.Repository;
using PoS.Order.Domain.Filters.Validators;
using PoS.Order.Domain.Wrappers;
using PoS.Order.Domain.Models;
using PoS.Order.Domain.Settings;

namespace PoS.Order.Data.Services
{
    /// <summary>
    /// Provides data-layer access to the <typeparamref name="OrderModel"/> table-repository.
    /// </summary>
    /// <typeparam name="OrderModel">The table-repository.</typeparam>
    public sealed class OrderRequestTrackingService<OrderModel> : IOrderRequestTrackingService<OrderModel> where OrderModel : OrderType
    {
        private readonly IOrderRequestRepository orderRequestRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="OrderRequestTrackingService{OrderType}"/> bound with the specified repository object reference.
        /// </summary>
        /// <param name="serviceScopeRepository"></param>
        public OrderRequestTrackingService(IOrderRequestRepository serviceScopeRepository) => this.orderRequestRepository = serviceScopeRepository;

        /// <inheritdoc/>
        public async Task<IDbOperation<IEnumerable<OrderModel>>> GetOrderRequestsAsync()
        {
            if(Repository<IOrderRequestRepository>.HasTableFor(tableType: typeof(OrderModel), orm: ORM.EntityFramewokCore))
            {
                IDbOperation<Object> onTableSearching = Repository<IOrderRequestRepository>.GetTableReferenceFor<OrderModel>(this.orderRequestRepository, orm: ORM.EntityFramewokCore);

                if(onTableSearching.OperationStatus == DbOperationsStatus.Success && onTableSearching.HasOperationUnderlyingResult())
                {
                    IDbOperation<DbSet<OrderModel>> tableSet = Repository<IOrderRequestRepository>
                        .GetTableThroughReferenceFor<OrderModel>(onTableSearching.EnsureInstance(), orm: ORM.EntityFramewokCore);

                    return new DbOperation<IEnumerable<OrderModel>>(await tableSet.EnsureInstance().ToListAsync<OrderModel>(), DbOperationsStatus.Success);
                }
            }
            return new DbOperation<IEnumerable<OrderModel>>(operationsStatus: DbOperationsStatus.Unallowed);
        }

        /// <inheritdoc/>
        public async Task<IDbOperation<OrderModel>> GetOrderRequestAsync(Guid id)
        {
            if (Repository<IOrderRequestRepository>.HasTableFor(tableType: typeof(OrderModel), orm: ORM.EntityFramewokCore))
            {
                IDbOperation<Object> onTableSearching = Repository<IOrderRequestRepository>.GetTableReferenceFor<OrderModel>(this.orderRequestRepository, orm: ORM.EntityFramewokCore);

                if (onTableSearching.OperationStatus == DbOperationsStatus.Success && onTableSearching.HasOperationUnderlyingResult())
                {
                    IDbOperation<DbSet<OrderModel>> tableSet = Repository<IOrderRequestRepository>
                        .GetTableThroughReferenceFor<OrderModel>(onTableSearching.EnsureInstance(), orm: ORM.EntityFramewokCore);

                    return new DbOperation<OrderModel>(await tableSet.EnsureInstance().FirstOrDefaultAsync<OrderModel>(entry => entry.Id == id), DbOperationsStatus.Success);
                }
            }
            return new DbOperation<OrderModel>(operationsStatus: DbOperationsStatus.Unallowed);
        }

        /// <inheritdoc/>
        public async Task<IDbOperation<WrapperFor<Boolean>>> RegisterOrderRequestAsync(OrderModel orderRequest)
        {
            IDbOperation<OrderModel> onOrderSearching = await this.GetOrderRequestAsync(orderRequest.Id);

            if(onOrderSearching.OperationStatus == DbOperationsStatus.Success && !onOrderSearching.HasOperationUnderlyingResult())
            {
                IDbOperation<Object> onTableSearching = Repository<IOrderRequestRepository>.GetTableReferenceFor<OrderModel>(this.orderRequestRepository, orm: ORM.EntityFramewokCore);

                IDbOperation<DbSet<OrderModel>> tableSet = Repository<IOrderRequestRepository>
                    .GetTableThroughReferenceFor<OrderModel>(onTableSearching.EnsureInstance(), orm: ORM.EntityFramewokCore);

                Boolean orderRegistered = await (await tableSet.EnsureInstance().AddAsync(orderRequest)).Context.SaveChangesAsync() > 0;

                if (orderRegistered)
                    return new DbOperation<WrapperFor<Boolean>>(new WrapperFor<Boolean>(true), DbOperationsStatus.Success);
            }
            return new DbOperation<WrapperFor<Boolean>>(new WrapperFor<Boolean>(false), DbOperationsStatus.Failed);
        }
    }
}
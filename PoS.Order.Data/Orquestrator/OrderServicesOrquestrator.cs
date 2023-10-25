using PoS.Order.Data.Services;
using PoS.Order.Domain.Wrappers;
using PoS.Order.Domain.Filters.Validators;
using PoS.Order.Domain.Interfaces.Services;
using PoS.Order.Domain.Interfaces.Repository;
using PoS.Order.Domain.Models;

namespace PoS.Order.Data.Orquestrator
{
    /// <summary>
    /// 
    /// </summary>
    public static class OrderServicesOrquestrator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ServiceScope"></typeparam>
        /// <typeparam name="OrderModel"></typeparam>
        /// <param name="correspondingServiceRepository"></param>
        /// <returns></returns>
        public static OrderServiceWrapper<ServiceScope> Request<ServiceScope, OrderModel>(IOrderRequestRepository correspondingServiceRepository)
            where ServiceScope : IOrderRequestTrackingService<OrderModel> where OrderModel : OrderType
        {
            if (typeof(ServiceScope).Equals<OrderRequestTrackingService<OrderModel>>() && typeof(ServiceScope).HasGenericTypeArgument<DefaultOrderRequest>())
            {
                return new OrderServiceWrapper<ServiceScope>(serviceInstance: new OrderRequestTrackingService<DefaultOrderRequest>(correspondingServiceRepository));
            }

            return new OrderServiceWrapper<ServiceScope>();
        }
    }
}
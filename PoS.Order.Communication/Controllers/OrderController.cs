using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PoS.Order.Data.Services;
using PoS.Order.Domain.Wrappers;
using PoS.Order.Domain.Settings;
using PoS.Infra.Order.Repository;
using PoS.Order.Domain.Models;
using PoS.Infra.Domain.Interfaces;
using PoS.Order.Domain.Interfaces.Services;
using PoS.Order.Domain.Interfaces.Repository;
using PoS.Order.Domain.Interfaces.Communication;
using PoS.Order.Data.Orquestrator;
using PoS.Infra.Messaging.RabbitMQClient.Events;
using PoS.Infra.Messaging.RabbitMQClient.Interfaces;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Models;
using PoS.Infra.Messaging.RabbitMQClient.Handlers;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Interfaces;
using PoS.Infra.Messaging.Domain.RabbitMQClient.Settings;
using PoS.Order.Domain.Interfaces.Wrapping;

namespace PoS.Order.Communication.Controllers
{
    /// <summary>
    /// Exposes the PoS Order-Service controller end-points.
    /// </summary>
    [ApiController, Route(template: "[controller]", Name = "Order Controller")]
    public sealed class OrderController : ControllerBase, IOrderEndPoints
    {
        private readonly IOrderRequestTrackingService<DefaultOrderRequest> orderRequestTrackingService;
        private readonly IRabbitMQInterceptionService rabbitMQInterceptionService;

        /// <summary>
        /// Initializes a new instance of <see cref="OrderController"/> with the specified service.
        /// </summary>
        /// <param name="serviceContext">A PoS context-centerd database-access service.</param>
        /// <param name="rabbitMQInterceptionService">A RabbitMQInterception service.</param>
        public OrderController(IPoSServiceDbContext<OrderRequestRepository> serviceContext, IRabbitMQInterceptionService rabbitMQInterceptionService)
        {
            this.orderRequestTrackingService = OrderServicesOrquestrator
                    .Request<OrderRequestTrackingService<DefaultOrderRequest>, DefaultOrderRequest>(correspondingServiceRepository: serviceContext.Repository).Instance;

            this.rabbitMQInterceptionService = rabbitMQInterceptionService;
        }

        /// <summary>
        /// Throws a new order.
        /// </summary>
        /// <param name="order">The order to be thrown.</param>
        /// <returns>An <see cref="IActionResult"/> with the runtime <see cref="StatusCodeResult"/>.</returns>
        [HttpPost(template: "throw-out", Name = "ThrowOrderAsync", Order = 0)]
        public async Task<IActionResult> ThrowOrderAsync([Required, FromBody]DefaultOrderRequestInputModel order)
        {
            DefaultOrderRequest orderRequest = new(category: order.Category, inquirer: order.Inquirer, instructions: order.Instructions);
            
            IDbOperation<WrapperFor<Boolean>> onOrderRequestRegistering = await this.orderRequestTrackingService.RegisterOrderRequestAsync(orderRequest);

            if(onOrderRequestRegistering.OperationStatus == DbOperationsStatus.Success)
            {
                // "IEventManager" => "EventSettings"
                IEventManager eventManager = new EventManager
                (
                    workingExchange: "Kitchen", workingQueue: order.Category.ToString(), exchangeQueueBindingKey: $"kitchen.{order.Category}", dispatchType: DispatchType.Direct
                );
                
                // "IThrowOrderEventHoster" => "Event"
                IThrowOrderEventHoster<DefaultOrderRequest> throwOrderEvent = new ThrowOrderEventHoster<DefaultOrderRequest>(eventOwner: EventOwnership.Kitchen,
                    eventManager: eventManager, eventContent: orderRequest);

                "IEventDealer" => "EventProcessor" | "rabbitMQInterceptionService" => "Transporter: RabbitMQ"
                IEventDealer throwOrderEventDealer = new EventDealer(rabbitMQInterceptionService: this.rabbitMQInterceptionService);

                IWrapperFor<Boolean> eventProcessingOperationSuccess = throwOrderEventDealer.Process<DefaultOrderRequest>(@event: throwOrderEvent);

                return this.Ok(orderRequest);
            }

            return this.Conflict();
        }

        /// <summary>
        /// Query and brings all orders.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/>.</returns>
        [HttpGet(template: "{id:Guid}", Name = "InquiryOrderAsync", Order = 1)]
        public async Task<IActionResult> InquiryOrderAsync([Required]Guid id)
        {
            IDbOperation<DefaultOrderRequest> onOrdersSearching = await this.orderRequestTrackingService.GetOrderRequestAsync(id);

            if (onOrdersSearching.OperationStatus == DbOperationsStatus.Success && onOrdersSearching.HasOperationUnderlyingResult())
                return this.Ok(onOrdersSearching.Result);

            return this.NotFound();
        }
    }
}

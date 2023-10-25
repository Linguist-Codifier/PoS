using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PoS.Infra.Context;
using PoS.Infra.Domain.Interfaces;
using PoS.Infra.Messaging.RabbitMQClient.Interfaces;
using PoS.Infra.Messaging.RabbitMQClient.SendingServices;
using PoS.Infra.Order.Repository;

namespace PoS.IoC.Services.PipeLines
{
    public static class PoSServices
    {
        public static void Initiate(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<OrderRequestRepository>(context => {
                //context.UseSqlServer(Builder.Configuration.GetConnectionString("DatabaseConnectionString")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                context.UseInMemoryDatabase("order_requests").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            /* Learning more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle */
            serviceCollection.AddControllers();
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddSwaggerGen();

            serviceCollection.AddScoped(typeof(IPoSServiceDbContext<OrderRequestRepository>), typeof(PoSServiceDbContext<OrderRequestRepository>));
            serviceCollection.AddScoped(typeof(IRabbitMQInterceptionService), typeof(RabbitMQInterceptionService));
        }
    }
}
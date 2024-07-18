using Lanches.Application.Commands.Orders.CreateOrder;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Lanches.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddMediatR(typeof(CreateOrderCommand));

            return services;
        }

        //private static IServiceCollection AddConsumers(this IServiceCollection services) {
        //    services.AddHostedService<PaymentApprovedConsumer>();

        //    return services;
        //}
    }
}
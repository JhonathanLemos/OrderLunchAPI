using Lanches.Application.ChainOfResponsibility;
using Lanches.Application.Services;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Lanches.Core.Services;
using Lanches.Infra.CacheStorage;
using Lanches.Infraestructure.Auth;
using Lanches.Infraestructure.MessageBus;
using Lanches.Infraestructure.Repositories;
using Lanches.Infraestructure.Subscribers;
using RabbitMQ.Client;

namespace Lanches.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderOrchestrator, OrderOrchestrator>();
        services.AddScoped<IUpdateOrderOrchestrator, UpdateOrderOrchestrator>();
        services.AddTransient<CreateNewOrderHandler>();
        services.AddTransient<CreateLunchItemsHandler>();
        services.AddTransient<PublishEventsHandler>();
        services.AddTransient<UpdateOrderHandler>();
        services.AddTransient<UpdateLunchItemsHandler>();
        //services.AddTransient<PublishEventsHandler>();
        return services;
    }

    public static IServiceCollection AddGenericsRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGenericRepository<Ingredient>, GenericRepository<Ingredient>>();
        services.AddScoped<IGenericRepository<Lunch>, GenericRepository<Lunch>>();
        services.AddScoped<IGenericRepository<LunchIngredient>, GenericRepository<LunchIngredient>>();
        services.AddScoped<IGenericRepository<LunchItem>, GenericRepository<LunchItem>>();
        return services;
    }
    public static IServiceCollection AddMessageBus(this IServiceCollection services)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
        };
        var connection = connectionFactory.CreateConnection();
        services.AddSingleton(new ProducerConnection(connection));
        services.AddSingleton<IMessageBusClient, RabbitMqClient>();
        return services;
    }

    public static IServiceCollection AddSubscribers(this IServiceCollection services)
    {
        services.AddHostedService<OrderCreatedSubscriber>();
        return services;
    }

    public static IServiceCollection AddRedisCache(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = "LanchesCaching-";
            options.Configuration = "localhost:6379";
        });
        services.AddTransient<ICacheService, CacheService>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateLunchService, CreateLunchService>();
        services.AddScoped<IUpdateLunchService, UpdateLunchService>();
        return services;
    }
}

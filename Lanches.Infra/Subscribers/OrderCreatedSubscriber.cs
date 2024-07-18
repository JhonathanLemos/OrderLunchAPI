using Lanches.Application.Services;
using Lanches.Core.Events;
using Lanches.Core.Repositories;
using Lanches.Infraestructure.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Lanches.Core.Resources;

namespace Lanches.Infraestructure.Subscribers
{
    public class OrderCreatedSubscriber : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string Queue = "order-service/order-created";
        private const string Exchange = "order-service";
        private const string RoutingKey = "order-created";


        public OrderCreatedSubscriber(IServiceProvider serviceProvider, ProducerConnection producerConnection)
        {
            _serviceProvider = serviceProvider;
            _connection = producerConnection.Connection;
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",

            };
            _connection = connectionFactory.CreateConnection("order-service");
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(Exchange, "topic", true);
            _channel.QueueDeclare(Queue, false, false, false);
            _channel.QueueBind(Queue, Exchange, RoutingKey);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();

                var contentString = Encoding.UTF8.GetString(byteArray);
                var message = JsonConvert.DeserializeObject<OrderCreated>(contentString);
                var result = await UpdateOrder(message);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(Queue, false, consumer);
            return Task.CompletedTask;
        }

        private async Task<bool> UpdateOrder(OrderCreated orderCreated)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var orderRepository = scope.ServiceProvider.GetService<IOrderRepository>();
                var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
                var order = await orderRepository.GetOrderById(orderCreated.Id);
                var user = await userRepository.GetByIdAsync(Guid.Parse(orderCreated.UserId));
                order.SetAsCompleted();
                await orderRepository.Update(order);
                await new IdentidadeService().SendEmailAsync(user, Resource.OrderPreparationSubject, Resource.OrderPreparationBody);

                return true;
            }
        }

        public class OrderReceived
        {
            public long Id { get; set; }
            public decimal TotalPrice { get; set; }
        }
    }
}

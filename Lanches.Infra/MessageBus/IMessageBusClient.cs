namespace Lanches.Infraestructure.MessageBus
{
    public interface IMessageBusClient
    {
        void Publish(object message, string routingKey, string exchange);
    }
}

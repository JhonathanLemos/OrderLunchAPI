using RabbitMQ.Client;

namespace Lanches.Infraestructure.MessageBus
{
    public class ProducerConnection
    {
        public ProducerConnection(IConnection connection)
        {
            Connection = connection;
        }

        public IConnection Connection { get; set; }
    }
}

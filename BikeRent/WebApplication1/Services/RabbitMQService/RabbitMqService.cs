using RabbitMQ.Client;
using System.Text;

namespace BikeRent.Services.RabbitMQService
{
    public class RabbitMqService : IDisposable
    {
        private const string QueueName = "motorcycles";
        private readonly IModel _channel;
        private readonly IConnection _connection;
        public RabbitMqService()
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Environment.GetEnvironmentVariable("RabbitMq__Host") ?? "localhost"
                };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false);
            }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }

        public IModel GetChannel() => _channel;

        public void Publish(string message)
        {
                var body = Encoding.UTF8.GetBytes(message);
                _channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: null, body: body);
        }
    }
}

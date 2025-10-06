using BikeRent.Data;
using BikeRent.Services.RabbitMQService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;



namespace BikeRent.Events
{
    public class MotorcycleConsumer2024 : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IModel _channel;
        private const string QueueName = "motorcycles";

        public MotorcycleConsumer2024(IServiceProvider serviceProvider, RabbitMqService rabbitMq)
        {
            _serviceProvider = serviceProvider;
            _channel = rabbitMq.GetChannel();

            _channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        }

        protected override async Task<Task> ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(3000, stoppingToken);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var evento = JsonSerializer.Deserialize<MotorcycleEvent>(message);

                if (evento?.Year == 2024)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<BikeRentDb>();
                    db.Add(new MotorcycleEvent2024
                    {
                        Id = evento.Id,
                        Year = evento.Year,
                        Model = evento.Model,
                        Plate = evento.Plate
                        
                    });
                    await db.SaveChangesAsync();
                }
            };

            _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private class MotorcycleEvent
        {
            public int Id { get; set; }
            public int Year { get; set; }
            public string Model { get; set; }
            public string Plate { get; set; }
        }

        
    }
}
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
        private const string QueueName = "motos";

        public MotorcycleConsumer2024(IServiceProvider serviceProvider, RabbitMqService rabbitMq)
        {
            _serviceProvider = serviceProvider;
            _channel = rabbitMq.GetChannel();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var evento = JsonSerializer.Deserialize<MotorcycleEvent>(message);

                if (evento?.Ano == 2024)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<BikeRentDb>();
                    db.Add(new MotorcycleEvent2024
                    {
                        Identificador = evento.Identificador,
                        Modelo = evento.Modelo,
                        Placa = evento.Placa,
                        RecebidoEm = DateTime.UtcNow
                    });
                    await db.SaveChangesAsync();
                }
            };

            _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private class MotorcycleEvent
        {
            public Guid Identificador { get; set; }
            public int Ano { get; set; }
            public string Modelo { get; set; }
            public string Placa { get; set; }
        }

        private class MotorcycleEvent2024
        {
            public int Id { get; set; }
            public Guid Identificador { get; set; }
            public string Modelo { get; set; }
            public string Placa { get; set; }
            public DateTime RecebidoEm { get; set; }
        }
    }
}

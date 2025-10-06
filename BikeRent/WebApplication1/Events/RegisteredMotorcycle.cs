using BikeRent.Models;
using BikeRent.Services;
using BikeRent.Services.RabbitMQService;
using System.Text.Json;

namespace BikeRent.Events
{
    public class RegisteredMotorcycle
    {
        private readonly RabbitMqService _rabbitMq;

        public RegisteredMotorcycle(RabbitMqService rabbitMq)
        {
            _rabbitMq = rabbitMq;
        }

        public void Publish(Motorcycle motorcycle)
        {
            var evento = new
            {
                Id = motorcycle.Id,
                Year = motorcycle.Year,
                Model = motorcycle.Model,
                Plate = motorcycle.Plate
            };

            var json = JsonSerializer.Serialize(evento);
            _rabbitMq.Publish(json);
        }
    }
}
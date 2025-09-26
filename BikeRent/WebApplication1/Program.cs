using Microsoft.EntityFrameworkCore;
using BikeRent.Data;
using BikeRent.Events;
using BikeRent.Services;
using BikeRent.Services.RabbitMQService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Postgres
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<BikeRentDb>(options =>
options.UseNpgsql(connectionString));

//RabbitMQ
builder.Services.AddSingleton<RabbitMqService>();
builder.Services.AddHostedService<MotorcycleConsumer2024>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

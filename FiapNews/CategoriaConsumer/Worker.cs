using Aplicacao.DTOs;
using CategoriaConsumer.request;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CategoriaConsumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private IRequest _request;

    public Worker(ILogger<Worker> logger, IRequest request)
    {
        _logger = logger;
        _request = request;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "fila-categoria",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );


                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (sender, eventArgrs) =>
                {

                    var body = eventArgrs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _request.Post(message);

                    Console.WriteLine(message);
                };

                channel.BasicConsume(
                    queue: "fila-categoria",
                    autoAck: true,
                    consumer: consumer
                );
            }
            await Task.Delay(2000);
        }
    }
}
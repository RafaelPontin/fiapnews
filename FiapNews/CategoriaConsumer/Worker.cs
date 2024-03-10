using CategoriaConsumer.request;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CategoriaConsumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private IRequest _request;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public Worker(ILogger<Worker> logger, IRequest request)
    {
        _logger = logger;
        _request = request;

        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(
            queue: "fila-categoria",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgrs) =>
            {
                var body = eventArgrs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _request.Post(message);
                _channel.BasicAck(eventArgrs.DeliveryTag, false);

                Console.WriteLine(message);
            };

            _channel.BasicConsume(
                queue: "fila-categoria",
                autoAck: false,
                consumer: consumer
            );
        }
        await Task.Delay(500);
    }
}
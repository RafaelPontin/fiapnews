using Dominio.Entidades;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Aplicacao.DTOs;
using Microsoft.Extensions.Configuration;

namespace Aplicacao.Mensagem;

public class Rabbit : IRabbit
{
    private string _user;
    private string _password;
    private string _host;
    public Rabbit(IConfiguration configuration)
    {
        _host = configuration["rabbit-host"];
        _user = configuration["rabbit-user"];
        _password = configuration["rabbit-password"];
    }

    public void Send(object entity, string fila)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _host,
            UserName = _user,
            Password = _password
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(
                queue: fila,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var mensagem = JsonSerializer.Serialize(entity);

            var body = Encoding.UTF8.GetBytes(mensagem);

            channel.BasicPublish(
                    exchange: "",
                    routingKey: fila,
                    basicProperties: null,
                    body: body
                );
        }
    }
}

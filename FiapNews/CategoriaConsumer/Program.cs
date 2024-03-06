using CategoriaConsumer;
using CategoriaConsumer.request;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IRequest, Request>();
    })
    .Build();

host.Run();

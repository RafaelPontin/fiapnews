using FiapNews.Configuracao;
using Infraestrutura.Persistencia;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfig(builder.Configuration);

var app = builder.Build();

app.UseConfig();

app.Run();

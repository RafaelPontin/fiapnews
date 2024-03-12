using ConfigSite.Data;
using ConfigSite.Data.Repository;
using ConfigSite.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ConfiguracaoSiteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));


builder.Services.AddScoped<IConfiguracaoSiteRepository, ConfiguracaoSiteRepository>();
builder.Services.AddScoped<ConfiguracaoSiteContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ConfiguracaoSiteContext>();

    dbContext.Database.Migrate();    
}


app.Run();

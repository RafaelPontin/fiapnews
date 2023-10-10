using Aplicacao;
using Infraestrutura;

namespace FiapNews.Configuracao
{
    public static class ApiConfiguracao
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddControllers();
            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddServicosInfraEstrutura(configuration);
            services.AddServicosAplicacao();

            return services;
        }

        public static WebApplication UseConfig(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            return app;
        }
    }
}

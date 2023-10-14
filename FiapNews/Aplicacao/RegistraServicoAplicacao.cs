using Aplicacao.Contratos.Servico;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aplicacao
{
    public static class RegistraServicoAplicacao
    {
        public static IServiceCollection AddServicosAplicacao(this IServiceCollection services)
        {
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IAdministradorService, AdministradorService>();
            services.AddScoped<IRedeSocialService, RedeSocialService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

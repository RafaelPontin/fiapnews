using Aplicacao.Contratos.Persistencia;
using Infraestrutura.Persistencia;
using Infraestrutura.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestrutura
{
    public static class RegistraServicosInfraestrutura
    {
        public static IServiceCollection AddServicosInfraEstrutura(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FiapNewsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped(typeof(IAutorRepository), typeof(AutorRepository));
            services.AddScoped(typeof(IAssinanteRepository), typeof(AssinanteRepository));
            services.AddScoped(typeof(IUsuarioRepository), typeof(UsuarioRepository));
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<INoticiaRepository, NoticiaRepository>();

            return services;
        }

    }
}

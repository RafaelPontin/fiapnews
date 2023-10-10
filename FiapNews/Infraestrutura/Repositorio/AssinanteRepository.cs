using Aplicacao.Contratos.Persistencia;
using Dominio.Entidades;
using Infraestrutura.Persistencia;

namespace Infraestrutura.Repositorio
{
    public class AssinanteRepository : RepositoryBase<Assinante>, IAssinanteRepository
    {
        public AssinanteRepository(FiapNewsContext dbContext) : base(dbContext)
        {
        }
    }
}

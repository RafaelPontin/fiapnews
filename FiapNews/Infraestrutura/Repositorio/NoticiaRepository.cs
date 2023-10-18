using Aplicacao.Contratos.Persistencia;
using Dominio.Entidades;
using Infraestrutura.Persistencia;

namespace Infraestrutura.Repositorio
{
    public class NoticiaRepository : RepositoryBase<Noticia>, INoticiaRepository
    {
        public NoticiaRepository(FiapNewsContext dbContext) : base(dbContext)
        {   
        }
    }
}

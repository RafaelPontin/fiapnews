using Aplicacao.Contratos.Persistencia;
using Dominio.Entidades;
using Infraestrutura.Persistencia;

namespace Infraestrutura.Repositorio
{
    public class ComentarioRepository : RepositoryBase<Comentario>, IComentarioRepository
    {
        public ComentarioRepository(FiapNewsContext dbContext) : base(dbContext)
        {
        }
    }
}

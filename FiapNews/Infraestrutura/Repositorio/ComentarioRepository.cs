using Aplicacao.Contratos.Persistencia;
using Dominio.Entidades;
using Dominio.Enum;
using Infraestrutura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorio;

public class ComentarioRepository : RepositoryBase<Comentario>, IComentarioRepository
{
    public ComentarioRepository(FiapNewsContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Comentario>> GetAprovados()
    {
        return await _dbSet
            .Where(x => x.EstadoValidacao.Equals(EstadoValidacaoComentario.Aprovado))
            .ToListAsync();
    }

    public async Task<IEnumerable<Comentario>> GetComentario(EstadoValidacaoComentario estadoValidacao)
    {
        return await _dbSet
            .Where(x => x.EstadoValidacao.Equals(estadoValidacao))
            .ToListAsync();
    }

    public async Task<IEnumerable<Comentario>> GetComentarioPorNoticia(Guid idNoticia, EstadoValidacaoComentario estadoValidacao)
    {
        return await _dbSet
            .Where(x => x.Noticia.Id.Equals(idNoticia) && x.EstadoValidacao == estadoValidacao)
            .ToListAsync();
    }
}

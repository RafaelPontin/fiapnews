using Aplicacao.Contratos.Persistencia;
using Aplicacao.DTOs;
using Dominio.Enum;

namespace Aplicacao.Contratos.Servico;

public interface IComentarioService : IServiceBase<ComentarioDto>
{
    Task Aprovar(Guid idComentario, Guid idAdministrador);
    Task Reprovar(Guid idComentario, Guid idAdministrador, string motivo);
    Task<IEnumerable<ComentarioDto>> GetAprovados();
    Task<IEnumerable<ComentarioDto>> GetReprovados();
    Task<IEnumerable<ComentarioDto>> GetPendentes();
    Task<IEnumerable<ComentarioDto>> GetAprovadosPorNoticia(Guid idNoticia);
    Task<IEnumerable<ComentarioDto>> GetReprovadosPorNoticia(Guid idNoticia);
    Task<IEnumerable<ComentarioDto>> GetPendentesPorNoticia(Guid idNoticia);
}

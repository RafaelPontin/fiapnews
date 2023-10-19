using Aplicacao.DTOs;
using Dominio.Entidades;

namespace Aplicacao.Contratos.Servico;

public interface INoticiaService : IServiceBase<NoticiaDto>
{
    IList<NoticiaDto> ObterNoticiaCategoria(Guid idCategoria);
}

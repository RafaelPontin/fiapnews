using Aplicacao.DTOs;
using Dominio.Entidades;

namespace Aplicacao.Contratos.Servico;

public interface INoticiaService : IServiceBase<NoticiaDto>
{
    IList<Noticia> ObterNoticiaCategoria(Guid idCategoria);
}

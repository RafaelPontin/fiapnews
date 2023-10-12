using Aplicacao.DTOs;
using Dominio.ObjetosDeValor;

namespace Aplicacao.Contratos.Servico;
public interface ITagService : IServiceBase<TagDto>
{
    Tag ObterPorTextoAsync(string texto);
}

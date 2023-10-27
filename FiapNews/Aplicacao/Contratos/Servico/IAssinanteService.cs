using Aplicacao.DTOs;
using Dominio.Entidades;

namespace Aplicacao.Contratos.Servico
{
    public interface IAssinanteService : IServiceBase<AssinanteDto>, IUsuarioService<Assinante>
    {
        Task AssinarAsync(AssinarDto assinarDto);

    }

}

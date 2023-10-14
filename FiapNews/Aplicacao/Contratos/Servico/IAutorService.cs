using Aplicacao.DTOs;
using Dominio.Entidades;

namespace Aplicacao.Contratos.Servico
{
    public interface IAutorService : IServiceBase<AutorDto>, IUsuarioService<Autor>
    {
        
    }

}

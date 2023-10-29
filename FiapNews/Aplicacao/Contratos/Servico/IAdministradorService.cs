using Aplicacao.DTOs;
using Dominio.Entidades;

namespace Aplicacao.Contratos.Servico
{
    public interface IAdministradorService : IServiceBase<AdministradorDto>, IUsuarioService<Administrador>
    {        
    }
}

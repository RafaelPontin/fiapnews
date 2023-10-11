using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;

namespace FiapNews.Controllers
{
    public class AdministradorController : BaseController<Administrador, AdministradorDto, IAdministradorService>
    {
        private readonly IAdministradorService appService;

        public AdministradorController(IAdministradorService appService) : base(appService)
        {
            this.appService = appService;
        }

        
    }
}

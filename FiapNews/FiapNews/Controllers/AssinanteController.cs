using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;

namespace FiapNews.Controllers
{
    public class AssinanteController : BaseController<Assinante, AssinanteDto, IAssinanteService>
    {
        private readonly IAssinanteService appService;

        public AssinanteController(IAssinanteService appService) : base(appService)
        {
            this.appService = appService;
        }
    }
}

using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;

namespace FiapNews.Controllers
{
    public class AutorController : BaseController<Autor, AutorDto, IAutorService>
    {
        private readonly IAutorService appService;

        public AutorController(IAutorService appService) : base(appService)
        {
            this.appService = appService;
        }        
    }
}

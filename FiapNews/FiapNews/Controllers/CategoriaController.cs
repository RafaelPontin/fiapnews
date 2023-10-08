using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.ObjetosDeValor;

namespace FiapNews.Controllers
{
    public class CategoriaController : BaseController<Categoria, CategoriaDto, ICategoriaService>
    {
        private readonly ICategoriaService appService;

        public CategoriaController(ICategoriaService appService) : base(appService)
        {
            this.appService = appService;
        }
    }
}

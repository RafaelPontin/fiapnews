using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.ObjetosDeValor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR")]
    public class CategoriaController : BaseController<Categoria, CategoriaDto, ICategoriaService>
    {
        private readonly ICategoriaService appService;

        public CategoriaController(ICategoriaService appService) : base(appService)
        {
            this.appService = appService;
        }

        [AllowAnonymous]
        public override Task<IActionResult> AdicionarAsync(CategoriaDto dto)
        {
            return base.AdicionarAsync(dto);
        }
    }
}

using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.ObjetosDeValor;
using Microsoft.AspNetCore.Authorization;

namespace FiapNews.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR")]
    public class RedeSocialController : BaseController<RedeSocial, RedeSocialDto, IRedeSocialService>
    {
        private readonly IRedeSocialService appService;

        public RedeSocialController(IRedeSocialService appService) : base(appService)
        {
            this.appService = appService;
        }
    }

}

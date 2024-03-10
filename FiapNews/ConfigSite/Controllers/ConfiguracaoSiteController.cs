using ConfigSite.DTO;
using ConfigSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConfigSite.Controllers
{
    [ApiController]
    public class ConfiguracaoSiteController : Controller
    {
        private readonly IConfiguracaoSiteRepository _configuracaoSiteRepository;

        public ConfiguracaoSiteController(IConfiguracaoSiteRepository configuracaoSiteRepository)
        {
            _configuracaoSiteRepository = configuracaoSiteRepository;
        }

        [AllowAnonymous]
        [HttpPost("configuracaosite/adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] ConfiguracaoSiteRequest request)
        {
            try
            {                
                var config = _configuracaoSiteRepository.Novo(request);
                config.DefinirLink();
                await _configuracaoSiteRepository.Adicionar(config);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Não foi possível adicionar a categoria.\n {ex.Message}");
                
            }            
        }        
    }
}

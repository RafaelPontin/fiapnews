using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.ObjetosDeValor;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers;
public class TagController : BaseController<Tag, TagDto, ITagService>
{
    private readonly ITagService appService;
    public TagController(ITagService appService) : base(appService)
    {
        this.appService = appService;
    }

    [HttpGet("Obter-Por-Texto/{texto}")]
    public async Task<IActionResult> ObterPorTexto(string texto)
    {
        try
        {
            var tag = Service.ObterPorTextoAsync(texto);
            if (tag == null) NoContent();
            return Ok(tag);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

}
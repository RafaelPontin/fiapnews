using Aplicacao;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers;

[Authorize(Roles = "AUTOR, ADMINISTRADOR")]
public class NoticiaController : BaseController<Noticia, NoticiaDto, INoticiaService>
{

    public NoticiaController(INoticiaService appService) : base(appService) { }

    [HttpGet("Obter-Por-Categoria/{id}")]
    public IActionResult ObterNoticiaPorCategoria(Guid id)
    {
        return Ok(Service.ObterNoticiaCategoria(id));
    }

    [AllowAnonymous]
    public override Task<IActionResult> ObterPorIdAsync(Guid id)
    {
        return base.ObterPorIdAsync(id);
    }

    [AllowAnonymous]
    public override Task<IActionResult> ObterTodosAsync()
    {
        return base.ObterTodosAsync();
    }

}
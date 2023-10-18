using Aplicacao;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers;
public class NoticiaController : BaseController<Noticia, NoticiaDto, INoticiaService>
{
    public NoticiaController(INoticiaService appService) : base(appService){}

    [HttpGet("Obter-Por-Categoria/{id}")]
    public IActionResult ObterNoticiaPorCategoria(Guid id)
    {
        return Ok(Service.ObterNoticiaCategoria(id));
    }
}

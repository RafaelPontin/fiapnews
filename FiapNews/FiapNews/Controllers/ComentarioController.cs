using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers;

public class ComentarioController : BaseController<Comentario, ComentarioDto, IComentarioService>
{
    private readonly IComentarioService _appService;

    public ComentarioController(IComentarioService appService) : base(appService)
    {
        _appService = appService;
    }

    //Validar ou Reprovar comentario
    [HttpPut("Aprovar/{idComentario}")]
    public async Task<IActionResult> Aprovar([FromRoute] Guid idComentario, [FromHeader] Guid idAdministrador)
    {
        try
        {
            await _appService.Aprovar(idComentario, idAdministrador);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("Reprovar/{idComentario}")]
    public async Task<IActionResult> Reprovar([FromRoute] Guid idComentario, [FromHeader] Guid idAdministrador, [FromBody] string motivo)
    {
        try
        {
            await _appService.Reprovar(idComentario, idAdministrador, motivo);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    //BuscarTodos
    [HttpGet("Aprovados")]
    public async Task<IActionResult> GetAprovados()
    {
        try
        {
            var comentarios = await _appService.GetAprovados();
            return Ok(comentarios);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [HttpGet("Reprovados")]
    public async Task<IActionResult> GetReprovados()
    {
        try
        {
            var comentarios = await _appService.GetReprovados();
            return Ok(comentarios);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [HttpGet("Pendentes")]
    public async Task<IActionResult> GetPendentes()
    {
        try
        {
            var comentarios = await _appService.GetPendentes();
            return Ok(comentarios);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    //BuscarPorNoticia
    [HttpGet("Aprovados/{idNoticia}")]
    public async Task<IActionResult> GetAprovadosPorNoticia([FromRoute] Guid idNoticia)
    {
        try
        {
            var comentarios = await _appService.GetAprovadosPorNoticia(idNoticia);
            return Ok(comentarios);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [HttpGet("Pendentes/{idNoticia}")]
    public async Task<IActionResult> GetPendentesPorNoticia([FromRoute] Guid idNoticia)
    {
        try
        {
            var comentarios = await _appService.GetPendentesPorNoticia(idNoticia);
            return Ok(comentarios);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [HttpGet("Reprovados/{idNoticia}")]
    public async Task<IActionResult> GetReprovadosPorNoticia([FromRoute] Guid idNoticia)
    {
        try
        {
            var comentarios = await _appService.GetReprovadosPorNoticia(idNoticia);
            return Ok(comentarios);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}

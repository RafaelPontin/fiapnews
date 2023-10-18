﻿using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs.Comentario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioController : ControllerBase
{
    private readonly IComentarioService _appService;

    public ComentarioController(IComentarioService appService)
    {
        _appService = appService;
    }

    //Validar ou Reprovar comentario
    [Authorize(Roles = "ADMINISTRADOR")]
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

    [Authorize(Roles = "ADMINISTRADOR")]
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

    //MetodosBasicos
    [Authorize(Roles = "ADMINISTRADOR")]
    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        try
        {
            var comentarios = await _appService.ObterTodosAsync();
            return Ok(comentarios);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
    {
        try
        {
            var comentario = await _appService.ObterPorIdAsync(id);
            return Ok(comentario);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AdicionarAsync([FromBody] ComentarioDto comentarioDTO)
    {
        try
        {
            await _appService.AdicionarAsync(comentarioDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar([FromRoute] Guid id)
    {
        try
        {
            await _appService.DeletarAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}

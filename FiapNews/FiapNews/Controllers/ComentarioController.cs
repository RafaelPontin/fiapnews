using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs.Comentario;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FiapNews.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "ADMINISTRADOR")]
public class ComentarioController : ControllerBase
{
    private readonly IComentarioService _appService;

    public ComentarioController(IComentarioService appService)
    {
        _appService = appService;
    }

    //Validar ou Reprovar comentario
    [HttpPut("AprovarComentario/{id}")]
    public async Task<IActionResult> Aprovar(Guid id)
    {
        try
        {
            var idAdministrador = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            await _appService.Aprovar(id, idAdministrador);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("ReprovarComentario/{id}")]
    public async Task<IActionResult> Reprovar(Guid id, [FromBody] ComentarioModerarDto dto)
    {
        try
        {
            var idAdministrador = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            await _appService.Reprovar(id, idAdministrador, dto.Motivo);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    //BuscarTodos
    [AllowAnonymous]
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
    [AllowAnonymous]
    [HttpGet("Aprovados/Obter-Por-Noticia/{id}")]
    public async Task<IActionResult> GetAprovadosPorNoticia(Guid id)
    {
        try
        {
            var comentarios = await _appService.GetAprovadosPorNoticia(id);
            return Ok(comentarios);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("Pendentes/Obter-Por-Noticia/{id}")]
    public async Task<IActionResult> GetPendentesPorNoticia(Guid id)
    {
        try
        {
            var comentarios = await _appService.GetPendentesPorNoticia(id);
            return Ok(comentarios);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("Reprovados/Obter-Por-Noticia/{id}")]
    public async Task<IActionResult> GetReprovadosPorNoticia(Guid id)
    {
        try
        {
            var comentarios = await _appService.GetReprovadosPorNoticia(id);
            return Ok(comentarios);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    //Metodos CRD
    [HttpGet("Obter-Todos")]
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

    [HttpGet("Obter-Por-Id/{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
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

    //[Authorize(Roles = "AUTOR, ADMINISTRADOR, ASSINANTE")]

    [AllowAnonymous]
    [HttpPost("Adicionar")]
    public async Task<IActionResult> AdicionarAsync([FromBody] ComentarioDto comentarioDTO)
    {
        try
        {
            var usuarioId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            await _appService.AdicionarAsync(comentarioDTO, usuarioId, role);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("Deletar/{id}")]
    public async Task<IActionResult> Deletar( Guid id)
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

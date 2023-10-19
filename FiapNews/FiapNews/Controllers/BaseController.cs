using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BaseController<TEntity, TDto, TService> : ControllerBase
    where TEntity : Base
    where TDto : BaseDto
    where TService : IServiceBase<TDto>
{
    protected TService Service;

    public BaseController(TService appService)
    {
        Service = appService;
    }

    [HttpPost("Adicionar")]
    public virtual async Task<IActionResult> AdicionarAsync(TDto dto)
    {
        try
        {
            await Service.AdicionarAsync(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("Alterar")]
    public async Task<IActionResult> AlterarAsync(TDto dto)
    {
        try
        {
            await Service.AlterarAsync(dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("Obter-Todos")]
    public virtual async Task<IActionResult> ObterTodosAsync()
    {            
        return Ok(await Service.ObterTodosAsync());
    }

    [HttpGet("Obter-Por-Id")]
    public virtual async Task<IActionResult> ObterPorIdAsync(Guid id)
    {
        return Ok(await Service.ObterPorIdAsync(id));
    }

    [HttpDelete("Deletar")]
    public async Task<IActionResult> DeletarAsync(Guid id)
    {            
        try
        {
            await Service.DeletarAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}

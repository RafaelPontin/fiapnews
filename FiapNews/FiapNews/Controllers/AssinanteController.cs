using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers
{
    public class AssinanteController : BaseController<Assinante, AssinanteDto, IAssinanteService>
    {
        private readonly IAssinanteService appService;

        public AssinanteController(IAssinanteService appService) : base(appService)
        {
            this.appService = appService;
        }

        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            try
            {
                await appService.AlterarSenha(alterarSenhaDto);
                return Ok("Senha alterado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("RecuperarSenha")]
        public async Task<IActionResult> RecuperarSenha(UsuarioSenhaDto usuarioSenhaDto)
        {
            try
            {
               await appService.RecuperarSenha(usuarioSenhaDto);
                return Ok("Senha recuperada com sucesso. Verifique o email de cadastro");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

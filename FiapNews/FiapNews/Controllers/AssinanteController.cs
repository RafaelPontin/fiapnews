﻿using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR")]
    public class AssinanteController : BaseController<Assinante, AssinanteDto, IAssinanteService>
    {
        private readonly IAssinanteService appService;

        public AssinanteController(IAssinanteService appService) : base(appService)
        {
            this.appService = appService;
        }

        [AllowAnonymous]
        public override Task<IActionResult> AdicionarAsync(AssinanteDto dto)
        {
            return base.AdicionarAsync(dto);
        }

        [Authorize(Roles = "ASSINANTE, ADMINISTRADOR")]
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
        [Authorize(Roles = "ASSINANTE, ADMINISTRADOR")]
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
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto usuario)
        {
            try
            {
                var token = appService.Autenticar(usuario);
                if (string.IsNullOrWhiteSpace(token))
                    return StatusCode(StatusCodes.Status400BadRequest, "Dados Informados inválidos");
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

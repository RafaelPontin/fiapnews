using Aplicacao.DTOs;

namespace Aplicacao.Contratos.Servico
{
    public interface IUsuarioService
    {
        Task AlterarSenha(AlterarSenhaDto alterarSenhaDto);
        Task RecuperarSenha(UsuarioSenhaDto usuarioSenhaDto);
    }

}

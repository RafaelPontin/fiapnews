using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;

namespace Aplicacao
{
    public class UsuarioService<TEntity> : IUsuarioService<TEntity> 
        where TEntity : Usuario
    {
        private readonly IRepositoryBase<Usuario> _usuarioRepository;

        public UsuarioService(IRepositoryBase<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            if (alterarSenhaDto == null || string.IsNullOrWhiteSpace(alterarSenhaDto.Senha))
                throw new Exception("Informe a senha");

            if ((!string.IsNullOrWhiteSpace(alterarSenhaDto.Senha) && !string.IsNullOrWhiteSpace(alterarSenhaDto.ConfirmacaoDeSenha))
                && (alterarSenhaDto.Senha != alterarSenhaDto.ConfirmacaoDeSenha))
                throw new Exception("Senha e Confirmação de senha não conferem");

            var usuario = _usuarioRepository.ObterIQueryable().OfType<TEntity>().Where(x => x.Login == alterarSenhaDto.Login).FirstOrDefault();
            if (usuario != null)
            {
                usuario.AlterarSenha(alterarSenhaDto.Senha);
                await _usuarioRepository.AlterarAsync(usuario);
                return;
            }
            throw new Exception("Login informado não encontrado");
        }

        public async Task RecuperarSenha(UsuarioSenhaDto usuarioSenhaDto)
        {
            if (usuarioSenhaDto == null || string.IsNullOrWhiteSpace(usuarioSenhaDto.Login))
                throw new Exception("Informe o login");
            var usuario = _usuarioRepository.ObterIQueryable().OfType<TEntity>().Where(x => x.Login == usuarioSenhaDto.Login).FirstOrDefault();
            if (usuario != null)
            {
                var novaSenha = usuario.GerarNovaSenha();
                var usuarioSenha = new RecuperarSenhaDto
                {
                    Login = usuario.Login,
                    Senha = novaSenha,
                };

                // TODO - Enviar a senha por email

                usuario.AlterarSenha(novaSenha);
                await _usuarioRepository.AlterarAsync(usuario);
                return;
            }

            throw new Exception("Login informado não encontrado");
        }

    }
}

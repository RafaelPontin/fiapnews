using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacao
{
    public class AdministradorService : ServiceBase<AdministradorDto, Administrador, IRepositoryBase<Administrador>>, IAdministradorService
    {
        private readonly IRepositoryBase<Administrador> _repository;
        public AdministradorService(IRepositoryBase<Administrador> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }

        protected override Administrador DefinirEntidadeInclusao(AdministradorDto dto)
        {
            var usuario = Repository.ObterIQueryable().FirstOrDefault(x => x.Login == dto.Login);

            if (usuario != null)            
                throw new Exception("Login informado não disponível.");
            
            return new Administrador(dto.Nome, dto.Login, dto.Senha, dto.Email, dto.Foto);
        }

        protected override void ValidarValores(AdministradorDto dto)
        {            
            if (dto == null)
                _erros.Add("Informe os dados do administrador.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                _erros.Add("Informe a descrição do administrador.");

            if (_erros.Any())
                throw new Exception(string.Join("\n", _erros));

            return;
        }

        protected override Administrador DefinirEntidadeAlteracao(Administrador entidade, AdministradorDto dto)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Administrador informada não encontrado.");
            

            return entidade;
        }
        protected override void ValidarDelecao(Administrador entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Administrador informada não encontrado.");
        }

        public async Task AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            if (alterarSenhaDto == null || string.IsNullOrWhiteSpace(alterarSenhaDto.Senha))
                throw new Exception("Informe a senha");

            if ((!string.IsNullOrWhiteSpace(alterarSenhaDto.Senha) && !string.IsNullOrWhiteSpace(alterarSenhaDto.ConfirmacaoDeSenha)) 
                && (alterarSenhaDto.Senha != alterarSenhaDto.ConfirmacaoDeSenha))
                throw new Exception("Senha e Confirmação de senha não conferem");

            var usuario = _repository.ObterIQueryable().Where(x => x.Login == alterarSenhaDto.Login).FirstOrDefault();
            if (usuario != null)
            {
                usuario.AlterarSenha(alterarSenhaDto.Senha);
                await _repository.AlterarAsync(usuario);
            }            
        }

        public async Task RecuperarSenha(UsuarioSenhaDto usuarioSenhaDto)
        {
            if (usuarioSenhaDto == null || string.IsNullOrWhiteSpace(usuarioSenhaDto.Login))
                throw new Exception("Informe o login");
            var usuario = _repository.ObterIQueryable().Where(x => x.Login == usuarioSenhaDto.Login).FirstOrDefault();
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
                await _repository.AlterarAsync(usuario);
                return;
            }

            throw new Exception("Login informado não encontrado");
        }
    }

}

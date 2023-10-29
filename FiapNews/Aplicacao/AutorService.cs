using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using AutoMapper;
using Dominio.Entidades;
using Dominio.ObjetosDeValor;

namespace Aplicacao
{
    public class AutorService : ServiceBase<AutorDto, Autor, IRepositoryBase<Autor>>, IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IRepositoryBase<RedeSocial> _redeSocialRepository;
        private readonly IUsuarioService<Autor> _usuarioService;

        public AutorService(
            IRepositoryBase<Autor> repository,
            IMapper mapper,
            IRepositoryBase<RedeSocial> redeSocialRepository,
            IAutorRepository autorRepository,
            IUsuarioService<Autor> usuarioService) : base(repository, mapper)
        {
            _redeSocialRepository = redeSocialRepository;
            _autorRepository = autorRepository;
            _usuarioService = usuarioService;
        }

        protected override Autor DefinirEntidadeInclusao(AutorDto dto)
        {
            var usuario = Repository.ObterIQueryable().FirstOrDefault(x => x.Login == dto.Login);

            if (usuario != null)
                throw new Exception("Login informado não disponível.");

            var autor = new Autor(dto.Nome, dto.Login, dto.Senha, dto.Email, dto.Foto, dto.Descricao);
            DefinirRedeSocial(autor, dto);
            return autor;
        }

        protected override void ValidarValores(AutorDto dto)
        {
            if (dto == null)
                _erros.Add("Informe os dados do autor.");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                _erros.Add("Informe o nome do autor.");

            if (string.IsNullOrWhiteSpace(dto.Login))
                _erros.Add("Informe o login do autor.");
            
            if (string.IsNullOrWhiteSpace(dto.Senha))
                _erros.Add("Informe a senha do autor.");
            
            if (string.IsNullOrWhiteSpace(dto.Email))
                _erros.Add("Informe a email do autor.");

            if (string.IsNullOrWhiteSpace(dto.Foto))
                _erros.Add("Informe a foto do autor.");

            if (string.IsNullOrWhiteSpace(dto.Descricao))
                _erros.Add("Informe a descricao do autor.");

            if (_erros.Any())
                throw new Exception(string.Join("\n", _erros));

            return;
        }

        protected override Autor DefinirEntidadeAlteracao(Autor entidade, AutorDto dto)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Autor informado não encontrada.");
            entidade.AlterarDadosDoUsuario(dto.Nome, dto.Email, dto.Foto);
            entidade.DefinirDescricao(dto.Descricao);
            DefinirRedeSocial(entidade, dto);
            return entidade;
        }
        protected override void ValidarDelecao(Autor entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Autor informada não encontrada.");
        }

        private void DefinirRedeSocial(Autor entidade, AutorDto dto)
        {
            if (!dto.RedesSociais.Any()) return;

            Task.Run(async () =>
            {
                foreach (var redeSocialDto in dto.RedesSociais)
                {
                    var redeSocial = await _redeSocialRepository.ObterPorIdAsync(redeSocialDto.Id);
                    if (redeSocial == null)
                    {
                        _erros.Add($"Rede social informada não encontrada. Id: {redeSocialDto.Id}");
                        continue;
                    }

                    if (entidade.RedesSociais != null && entidade.RedesSociais.Contains(redeSocial))
                    {
                        _erros.Add($"Rede social já informada: {redeSocialDto.Id}");
                        continue;
                    }
                    entidade.AdicionarRedeSocial(redeSocial);
                }
            }).Wait();

            if (_erros.Any())
                throw new Exception(string.Join("\n", _erros));
        }


        public override async Task<AutorDto> ObterPorIdAsync(Guid id)
        {            
            return _mapper.Map<AutorDto>(await _autorRepository.ObterAutorPorId(id));            
        }
        public override async Task<IReadOnlyList<AutorDto>> ObterTodosAsync()
        {            
            return _mapper.Map<IReadOnlyList<AutorDto>>(await _autorRepository.ObterAutores());            
        }
        public async Task AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            await _usuarioService.AlterarSenha(alterarSenhaDto);
        }

        public async Task RecuperarSenha(UsuarioSenhaDto usuarioSenhaDto)
        {
            await _usuarioService.RecuperarSenha(usuarioSenhaDto);
        }
        public string Autenticar(LoginDto loginDto)
        {
            return _usuarioService.Autenticar(loginDto);
        }
    }

}

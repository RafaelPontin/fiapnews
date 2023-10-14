﻿using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacao
{
    public class AssinanteService : ServiceBase<AssinanteDto, Assinante, IRepositoryBase<Assinante>>, IAssinanteService
    {
        private readonly IAssinanteRepository _repository;
        public AssinanteService(
            IAssinanteRepository repository, 
            IMapper mapper
            ) : base(repository, mapper)
        {
            _repository = repository;
        }

        protected override Assinante DefinirEntidadeInclusao(AssinanteDto dto)
        {
            var assinante = new Assinante(dto.Nome, dto.Login, dto.Senha, dto.Email, dto.Foto, null);
            return assinante;
        }

        protected override void ValidarValores(AssinanteDto dto)
        {
            if (dto == null)
                _erros.Add("Informe os dados do assinante.");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                _erros.Add("Informe o nome do assinante.");

            if (string.IsNullOrWhiteSpace(dto.Login))
                _erros.Add("Informe o login do assinante.");
            
            if (string.IsNullOrWhiteSpace(dto.Senha))
                _erros.Add("Informe a senha do assinante.");
            
            if (string.IsNullOrWhiteSpace(dto.Email))
                _erros.Add("Informe a email do assinante.");

            if (string.IsNullOrWhiteSpace(dto.Foto))
                _erros.Add("Informe a foto do assinante.");

            if (_erros.Any())
                throw new Exception(string.Join("\n", _erros));

            return;
        }

        protected override Assinante DefinirEntidadeAlteracao(Assinante entidade, AssinanteDto dto)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Assinante informado não encontrada.");
            entidade.DefinirUsuario(dto.Nome, dto.Login, entidade.Senha, dto.Email, dto.Foto, entidade.Tipo);
            return entidade;
        }
        protected override void ValidarDelecao(Assinante entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Assinante informada não encontrada.");
        }

        public override async Task<AssinanteDto> ObterPorIdAsync(Guid id)
        {            
            return _mapper.Map<AssinanteDto>(await _repository.ObterAssinantePorId(id));            
        }

        public override async Task<IReadOnlyList<AssinanteDto>> ObterTodosAsync()
        {            
            return _mapper.Map<IReadOnlyList<AssinanteDto>>(await _repository.ObterAssinantes());            
        }
    }

}

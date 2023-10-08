using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacao
{
    public class ServiceBase<TDto, TEntity, TRepository> : IServiceBase<TDto>
        where TDto : BaseDto
        where TEntity : Base
        where TRepository : IRepositoryBase<TEntity>
    {
        protected TRepository Repository;
        private readonly IMapper _mapper;
        protected List<string> _erros;
        public ServiceBase(TRepository repository, IMapper mapper)
        {
            Repository = repository;
            _mapper = mapper;
            _erros = new();
        }

        public async Task AdicionarAsync(TDto dto)
        {
            ValidarValores(dto);
            await Repository.AdicionarAsync(DefinirEntidadeInclusao(dto));
        }

        public async Task AlterarAsync(TDto dto)
        {
            ValidarValores(dto);
            var entidade = await Repository.ObterPorIdAsync(dto.Id);
            await Repository.AlterarAsync(DefinirEntidadeAlteracao(entidade, dto));
        }

        public async Task DeletarAsync(Guid id)
        {
            var entidade = await Repository.ObterPorIdAsync(id);
            ValidarDelecao(entidade);
            await Repository.DeletarAsync(entidade);
        }

        public IQueryable<TDto> ObterIQueryable()
        {
            throw new NotImplementedException();
        }

        public async Task<TDto> ObterPorIdAsync(Guid id)
        {
            return _mapper.Map<TDto>(await Repository.ObterPorIdAsync(id));
        }

        public virtual async Task<IReadOnlyList<TDto>> ObterTodosAsync()
        {                       
            return _mapper.Map<IReadOnlyList<TDto>>(await Repository.ObterTodosAsync());
        }

        protected virtual void ValidarValores(TDto dto)
        {
            throw new NotImplementedException();
        }
        protected virtual void ValidarDelecao(TEntity entidade)
        {
            throw new NotImplementedException();
        }

        protected virtual TEntity DefinirEntidadeInclusao(TDto dto)
        {
            throw new NotImplementedException();
        }

        protected virtual TEntity DefinirEntidadeAlteracao(TEntity entidade, TDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

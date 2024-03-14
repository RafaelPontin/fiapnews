using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.Mensagem;
using AutoMapper;
using Dominio.ObjetosDeValor;

namespace Aplicacao
{
    public class CategoriaService : ServiceBase<CategoriaDto, Categoria, IRepositoryBase<Categoria>>, ICategoriaService
    {
        private IRabbit _rabbit;
        private readonly IRepositoryBase<Categoria> _repository;
        public CategoriaService(IRepositoryBase<Categoria> repository, IMapper mapper, IRabbit rabbit) : base(repository, mapper)
        {
            _rabbit = rabbit;   
            _repository = repository;
        }

        public override async Task AdicionarAsync(CategoriaDto dto)
        {
            
            await base.AdicionarAsync(dto);

            _rabbit.Send(dto, "fila-categoria");
        }

        protected override Categoria DefinirEntidadeInclusao(CategoriaDto dto)
        {
            return new Categoria(dto.Descricao, dto.Id);
        }

        protected override void ValidarValores(CategoriaDto dto)
        {
            if (dto == null)
                _erros.Add("Informe os dados da categoria.");

            if (string.IsNullOrWhiteSpace(dto.Descricao))
                _erros.Add("Informe a descrição da categoria.");

            var entidade = _repository.ObterIQueryable().Where(x => x.Id == dto.Id).FirstOrDefault();
            if (entidade != null)
                _erros.Add("Id da categoria ja cadastrado.");

            if (_erros.Any())
                throw new Exception(string.Join("\n", _erros));
           
            return;
        }

        protected override Categoria DefinirEntidadeAlteracao(Categoria entidade, CategoriaDto dto)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Categoria informada não encontrada.");

            entidade.DefinirDescricao(dto.Descricao);

            return entidade;
        }
        protected override void ValidarDelecao(Categoria entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Categoria informada não encontrada.");
        }
    }
}

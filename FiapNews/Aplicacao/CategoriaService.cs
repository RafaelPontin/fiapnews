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

        public CategoriaService(IRepositoryBase<Categoria> repository, IMapper mapper, IRabbit rabbit) : base(repository, mapper)
        {
            _rabbit = rabbit;   
        }

        public override async Task AdicionarAsync(CategoriaDto dto)
        {
            //dto.Aprovado = false;
            await base.AdicionarAsync(dto);

            _rabbit.Send(dto, "fila-categoria");
        }

        protected override Categoria DefinirEntidadeInclusao(CategoriaDto dto)
        {
            return new Categoria(dto.Descricao);
        }

        protected override void ValidarValores(CategoriaDto dto)
        {
            if (dto == null)
                _erros.Add("Informe os dados da categoria.");

            if (string.IsNullOrWhiteSpace(dto.Descricao))
                _erros.Add("Informe a descrição da categoria.");

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

using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using AutoMapper;
using Dominio.ObjetosDeValor;

namespace Aplicacao
{
    public class CategoriaService : ServiceBase<CategoriaDto, Categoria, IRepositoryBase<Categoria>>, ICategoriaService
    {
        public CategoriaService(IRepositoryBase<Categoria> repository, IMapper mapper) : base(repository, mapper)
        {
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

using Aplicacao.DTOs;

namespace Aplicacao.Contratos.Servico
{
    public interface IServiceBase<TDto>
        where TDto : BaseDto
    {
        Task<IReadOnlyList<TDto>> ObterTodosAsync();
        Task<TDto> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(TDto dto);
        Task AlterarAsync(TDto dto);
        Task DeletarAsync(Guid id);
        IQueryable<TDto> ObterIQueryable();        
    }
}

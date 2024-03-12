using ConfigSite.DTO;

namespace ConfigSite.Models
{
    public interface IConfiguracaoSiteRepository
    {
        Task Adicionar(ConfiguracaoSite produto);
        ConfiguracaoSite Novo(ConfiguracaoSiteRequest request);
    }
}

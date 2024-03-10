using ConfigSite.DTO;
using ConfigSite.Models;

namespace ConfigSite.Data.Repository
{
    public class ConfiguracaoSiteRepository : IConfiguracaoSiteRepository
    {
        private readonly ConfiguracaoSiteContext _context;

        public ConfiguracaoSiteRepository(ConfiguracaoSiteContext context)
        {
            _context = context;
        }

        public async Task Adicionar(ConfiguracaoSite produto)
        {
            _context.ConfiguracaoSite.Add(produto);
            await _context.SaveChangesAsync();
        }

        public ConfiguracaoSite Novo(ConfiguracaoSiteRequest request)
        {
            var configSite = new ConfiguracaoSite
            {
                Id = Guid.NewGuid(),
                DataCriacao = DateTime.Now,
                Descricao = request.Descricao                
            };

            return configSite;
        }
    }
}

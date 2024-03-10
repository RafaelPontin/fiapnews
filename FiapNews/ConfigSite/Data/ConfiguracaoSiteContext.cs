using ConfigSite.Models;
using Microsoft.EntityFrameworkCore;

namespace ConfigSite.Data
{
    public class ConfiguracaoSiteContext : DbContext
    {
        public ConfiguracaoSiteContext(DbContextOptions<ConfiguracaoSiteContext> options)
            : base(options) { }

        public DbSet<ConfiguracaoSite> ConfiguracaoSite { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConfiguracaoSiteContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}

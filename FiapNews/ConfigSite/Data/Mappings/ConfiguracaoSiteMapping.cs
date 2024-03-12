using ConfigSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigSite.Data.Mappings
{
    public class ConfiguracaoSiteMapping
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoSite> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Link)
                .IsRequired()
                .HasColumnType("varchar(500)");

            
            builder.ToTable("ConfiguracaoSite");
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prova.Domain;

namespace Prova.Database.EntityConfigurations;

public class LocalizacaoEntityTypeConfiguration : IEntityTypeConfiguration<Localizacao>
{
    public void Configure(EntityTypeBuilder<Localizacao> builder)
    {
        builder.ToTable("tb_localizacao");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).HasColumnName("id_cidade");
        builder.Property(l => l.Nome).HasColumnName("nome");
        builder.Property(l => l.Categoria).HasColumnName("categoria");
        builder.Property(l => l.Local).HasColumnName("local");
    }
}
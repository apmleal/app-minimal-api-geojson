using Microsoft.EntityFrameworkCore;
using Prova.Database.EntityConfigurations;
using Prova.Domain;

namespace Prova.Database;

public sealed class ProvaContext(DbContextOptions<ProvaContext> options) : DbContext(options)
{   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //extensão que gera o GUID Temporal
        modelBuilder.HasPostgresExtension(null, "uuid-ossp", default);
        modelBuilder.HasPostgresExtension(null, "pg_trgm", default);
        modelBuilder.HasPostgresExtension(null, "unaccent", default);

        modelBuilder.ApplyConfiguration(new LocalizacaoEntityTypeConfiguration());
    }    
}


using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;
using Prova.Domain;

namespace Prova.Application.Modules.Localizacoes.Queries;

public record ListarPontosLocalizacoesQuery : IRequest<FeatureCollection>
{
}

public class ListarPontosLocalizacoesQueryHandler<TContext>(TContext context) : IRequestHandler<ListarPontosLocalizacoesQuery, FeatureCollection>
    where TContext : DbContext
{
    private readonly TContext _context = context;

    public async Task<FeatureCollection> Handle(ListarPontosLocalizacoesQuery request, CancellationToken cancellationToken)
    {
        var dbSet = _context.Set<Localizacao>();

        var localizacoes = await dbSet
          .Select(c => new
          {
              c.Id,
              c.Nome,
              c.Categoria,
              c.Local
          })
          .ToListAsync(cancellationToken: cancellationToken);


        var featureCollection = new FeatureCollection();

        foreach (var localizacao in localizacoes)
        {
            featureCollection.Add(new Feature(localizacao.Local!, new AttributesTable()
          {
                { "id", localizacao.Id },
                { "nome", localizacao.Nome },
                { "categoria", Categoria.FromValue(localizacao.Categoria) },
          }));
        }

        return featureCollection;
    }
}

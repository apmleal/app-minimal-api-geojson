using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Prova.Domain;

namespace Prova.Application.Modules.Localizacoes.Queries;

public record LocalizacaoPorIdQuery(Guid Id): IRequest<LocalizacaoPorIdDto?>
{
}

public class LocalizacaoPorIdQueryHandler<TContext>(TContext context) : IRequestHandler<LocalizacaoPorIdQuery, LocalizacaoPorIdDto?>
    where TContext : DbContext
{
    private readonly TContext _context = context;

    public async Task<LocalizacaoPorIdDto?> Handle(LocalizacaoPorIdQuery request, CancellationToken cancellationToken)
    {
        var dbSet = _context.Set<Localizacao>();

        var localizacao = await dbSet
            .Where(c => c.Id == request.Id)
            .Select(x => new LocalizacaoPorIdDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Local = x.Local

            })
            .FirstOrDefaultAsync(cancellationToken);

        return localizacao;
    }
}


public record LocalizacaoPorIdDto
{
    public Guid Id { get; set; }
    public string? Nome { get; init; }
    public Point? Local { get; set; }
}

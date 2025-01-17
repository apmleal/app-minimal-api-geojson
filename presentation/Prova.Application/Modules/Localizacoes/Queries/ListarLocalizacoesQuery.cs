using MediatR;
using Microsoft.EntityFrameworkCore;
using Prova.Domain;

namespace Prova.Application.Modules.Localizacoes.Queries;

public record ListarLocalizacoesQuery: IRequest<IEnumerable<LocalizacaoDto>>
{
}

public class ListarLocalizacoesQueryHandler<TContext>(TContext context) : IRequestHandler<ListarLocalizacoesQuery, IEnumerable<LocalizacaoDto>>
    where TContext : DbContext
{
    private readonly TContext _context = context;    

    public async Task<IEnumerable<LocalizacaoDto>> Handle(ListarLocalizacoesQuery request, CancellationToken cancellationToken)
    {
        var dbSet = _context.Set<Localizacao>();

        var localizacoes = await dbSet
            .Select(x => new LocalizacaoDto
            {
                Id = x.Id,
                Nome = x.Nome
            })
            .ToListAsync(cancellationToken);

        return localizacoes;
    }
}


public record LocalizacaoDto
{
    public Guid Id { get; set; }
    public string? Nome { get; init; } 
}
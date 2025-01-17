using MediatR;
using Prova.Domain;

namespace Prova.Application.Modules.Categorias.Queries;

public record ListarCategoriasQuery: IRequest<IEnumerable<CategoriaDto>>
{
}

public class ListarCategoriasQueryHandler() : IRequestHandler<ListarCategoriasQuery, IEnumerable<CategoriaDto>>    
{
    public Task<IEnumerable<CategoriaDto>> Handle(ListarCategoriasQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Categoria.AllCategories.Select(c => new CategoriaDto
        {
            Value = c!.Value,
            Nome = c.Name
        }));
    }
}


public record CategoriaDto
{
    public int Value { get; set; }
    public string? Nome { get; init; }
}

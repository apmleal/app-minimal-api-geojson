using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Prova.Application.Modules.Categorias.Endpoints;

namespace Prova.Application.Modules.Categorias;

public static class CategoriaEndpoints
{
    public static RouteGroupBuilder MapRoutesCategoria(this IEndpointRouteBuilder app)
    {
        var builder = app
            .MapGroup("categoria")
            .WithTags("Categorias");

        builder.MapMethods(GetCategorias.Pattern, GetCategorias.Methods, GetCategorias.Handle)
            .Produces(200)
            .Produces<IDictionary<string, IEnumerable<string>>>(400)
            .Produces<ProblemDetails>(500);


        return builder;
    }

}
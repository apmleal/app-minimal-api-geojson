using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prova.Application.Modules.Categorias.Queries;
using static System.Net.WebRequestMethods;

namespace Prova.Application.Modules.Categorias.Endpoints;

public static class GetCategorias
{
    public static string Pattern => "/";
    public static string[] Methods => [Http.Get];
    public static Delegate Handle => Get;

    public static async Task<IResult> Get([FromServices] IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ListarCategoriasQuery(), cancellationToken);

        return Results.Ok(result);
    }
}
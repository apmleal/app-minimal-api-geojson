using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prova.Application.Modules.Localizacoes.Queries;
using static System.Net.WebRequestMethods;

namespace Prova.Application.Modules.Localizacoes.Endpoints;

public static class GetPontosLocalizacao
{
    public static string Pattern => "/geojson";
    public static string[] Methods => [Http.Get];
    public static Delegate Handle => Get;

    public static async Task<IResult> Get([FromServices] IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ListarPontosLocalizacoesQuery(), cancellationToken);

        return Results.Ok(result);
    }
}
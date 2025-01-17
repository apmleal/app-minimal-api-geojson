using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prova.Application.Modules.Localizacoes.Queries;
using static System.Net.WebRequestMethods;

namespace Prova.Application.Modules.Localizacoes.Endpoints;

public static class GetLocalizacaoPorId
{
    public static string Pattern => "{id}";
    public static string[] Methods => [Http.Get];
    public static Delegate Handle => Get;

    public static async Task<IResult> Get([FromServices] IMediator mediator, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LocalizacaoPorIdQuery(id), cancellationToken);

        return Results.Ok(result);
    }
}
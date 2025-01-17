using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prova.Application.Modules.Localizacoes.Commands;

namespace Prova.Application.Modules.Localizacoes.Endpoints;

public static class DeleteLocalizacao
{
    public static string Pattern => "{id}";
    public static string[] Methods => ["DELETE"];
    public static Delegate Handle => Delete;

    public static async Task<IResult> Delete([FromServices] IMediator mediator, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new DeleteLocalizacaoCommand(id), cancellationToken);

        if (response.IsValid)
        {
            return Results.NoContent();
        }
        return Results.BadRequest(response.Erros);

    }
}

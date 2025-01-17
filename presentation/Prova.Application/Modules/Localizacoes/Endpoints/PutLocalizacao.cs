using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prova.Application.Modules.Localizacoes.Commands;
using static System.Net.WebRequestMethods;

namespace Prova.Application.Modules.Localizacoes.Endpoints;

public static class PutLocalizacao
{
    public static string Pattern => "{id}";
    public static string[] Methods => [Http.Put];
    public static Delegate Handle => Put;

    public static async Task<IResult> Put([FromServices] IMediator mediator, [FromRoute] Guid id, [FromBody] UpdateLocalizacaoModel model, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new UpdateLocalizacaoCommand(id, model.Nome, model.Categoria, model.Longitude, model.Latitude), cancellationToken);

        if (response.IsValid)
        {
            return Results.Created("/localizacao",response.Value);
        }
        return Results.BadRequest(response.Erros);

    }
}

public class UpdateLocalizacaoModel
{    
    public string? Nome { get; init; }
    public int Categoria { get; set; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }
}
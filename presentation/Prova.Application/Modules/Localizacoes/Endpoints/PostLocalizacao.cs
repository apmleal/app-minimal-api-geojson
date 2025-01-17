using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prova.Application.Modules.Localizacoes.Commands;
using static System.Net.WebRequestMethods;

namespace Prova.Application.Modules.Localizacoes.Endpoints;

public static class PostLocalizacao
{
    public static string Pattern => "/";
    public static string[] Methods => [Http.Post];
    public static Delegate Handle => Post;

    public static async Task<IResult> Post([FromServices] IMediator mediator, [FromBody] CreateLocalizacaoModel model, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new CreateLocalizacaoCommand(model.Nome, model.Categoria, model.Longitude, model.Latitude), cancellationToken);

        if (response.IsValid)
        {
            return Results.Created("/localizacao",response.Value);
        }
        return Results.BadRequest(response.Erros);

    }
}

public class CreateLocalizacaoModel
{    
    public string? Nome { get; init; }
    public int Categoria { get; set; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }
}
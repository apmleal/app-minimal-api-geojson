using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetTopologySuite.Features;
using Prova.Application.Modules.Localizacoes.Endpoints;
using Prova.Application.Modules.Localizacoes.Queries;

namespace Prova.Application.Modules.Localizacoes;

public static class LocalizacaoEndpoints
{
    public static RouteGroupBuilder MapRoutesLocalizacao(this IEndpointRouteBuilder app)
    {
        var builder = app
            .MapGroup("localizacao")
            .WithTags("Localização");

        builder.MapMethods(GetLocalizacao.Pattern, GetLocalizacao.Methods, GetLocalizacao.Handle)
            .Produces<IEnumerable<LocalizacaoDto>>(200)
            .Produces<IDictionary<string, IEnumerable<string>>>(400)
            .Produces<ProblemDetails>(500)
            .WithSummary("Rota para listagem básica das informações das localizações");

        builder.MapMethods(GetPontosLocalizacao.Pattern, GetPontosLocalizacao.Methods, GetPontosLocalizacao.Handle)
            .Produces<FeatureCollection>(200)
            .Produces<IDictionary<string, IEnumerable<string>>>(400)
            .Produces<ProblemDetails>(500)
            .WithSummary("Rota para listagem dos geojson (pontos) das localizações");

        builder.MapMethods(GetLocalizacaoPorId.Pattern, GetLocalizacaoPorId.Methods, GetLocalizacaoPorId.Handle)
            .Produces<LocalizacaoPorIdDto>(200)
            .Produces<IDictionary<string, IEnumerable<string>>>(400)
            .Produces<ProblemDetails>(500)
            .WithSummary("Rota para obter uma localização por ID");

        builder.MapMethods(PostLocalizacao.Pattern, PostLocalizacao.Methods, PostLocalizacao.Handle)
            .Produces<Guid>(201)
            .Produces<IDictionary<string, IEnumerable<string>>>(400)
            .Produces<ProblemDetails>(500)
            .WithSummary("Rota para cadastrar uma localização");

        builder.MapMethods(PutLocalizacao.Pattern, PutLocalizacao.Methods, PutLocalizacao.Handle)
            .Produces<Guid>(201)
            .Produces<IDictionary<string, IEnumerable<string>>>(400)
            .Produces<ProblemDetails>(500)
            .WithSummary("Rota para alterar uma localização");

        builder.MapMethods(DeleteLocalizacao.Pattern, DeleteLocalizacao.Methods, DeleteLocalizacao.Handle)
           .Produces(204)
           .Produces<IDictionary<string, IEnumerable<string>>>(400)
           .Produces<ProblemDetails>(500)
           .WithSummary("Rota para deletar uma localização");

        return builder;
    }

}

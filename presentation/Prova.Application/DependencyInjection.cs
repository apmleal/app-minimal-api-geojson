using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Features;
using Prova.Application.Core.Behaviors;
using Prova.Application.Core.Notifications;
using Prova.Application.Modules.Categorias;
using Prova.Application.Modules.Categorias.Queries;
using Prova.Application.Modules.Localizacoes;
using Prova.Application.Modules.Localizacoes.Commands;
using Prova.Application.Modules.Localizacoes.Queries;

namespace Prova.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesApplication<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddSingleton<NotificationContext>();
        services.RegisterQuerys<TContext>();
        services.RegisterCommands<TContext>();
        services.RegisterValidators<TContext>();

        return services;
    }


    public static void RegisterQuerys<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        //Localizacao
        services.AddScoped<IRequestHandler<ListarLocalizacoesQuery, IEnumerable<LocalizacaoDto>>, ListarLocalizacoesQueryHandler<TContext>>();
        services.AddScoped<IRequestHandler<ListarPontosLocalizacoesQuery, FeatureCollection>, ListarPontosLocalizacoesQueryHandler<TContext>>();
        services.AddScoped<IRequestHandler<LocalizacaoPorIdQuery, LocalizacaoPorIdDto?>, LocalizacaoPorIdQueryHandler<TContext>>();

        //Categorias
        services.AddScoped<IRequestHandler<ListarCategoriasQuery, IEnumerable<CategoriaDto>>, ListarCategoriasQueryHandler>();
    }

    public static void RegisterCommands<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        //Localizacao
        services.AddScoped<IRequestHandler<CreateLocalizacaoCommand, ResponseCommand>, CreateLocalizacaoCommandHandler<TContext>>();
        services.AddScoped<IRequestHandler<UpdateLocalizacaoCommand, ResponseCommand>, UpdateLocalizacaoCommandHandler<TContext>>();
        services.AddScoped<IRequestHandler<DeleteLocalizacaoCommand, ResponseCommand>, DeleteLocalizacaoCommandHandler<TContext>>();
    }

    public static void RegisterValidators<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
     
        services.AddScoped<IValidator<CreateLocalizacaoCommand>, CreateLocalizacaoCommandValidator>();
        services.AddScoped<IValidator<UpdateLocalizacaoCommand>, UpdateLocalizacaoCommandValidator>();
        services.AddScoped<IValidator<DeleteLocalizacaoCommand>, DeleteLocalizacaoCommandValidator>();
    }


    public static WebApplication AppBuilderAplication(this WebApplication app)
    {
        app.MapRoutesLocalizacao();
        app.MapRoutesCategoria();

        return app;
    }
}

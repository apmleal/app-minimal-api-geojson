using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.IO.Converters;
using Prova.Application;
using Prova.Application.Core.Behaviors;
using Prova.Application.Core.Exceptions;
using Prova.Database;
using System.Reflection;
using System.Text.Json.Serialization;


namespace Prova.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            var geoJsonConverterFactory = new GeoJsonConverterFactory(writeGeometryBBox: true);
            options.SerializerOptions.Converters.Add(geoJsonConverterFactory);
        });

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddDbContext<ProvaContext>((options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DbConnection"), o => o.UseNetTopologySuite());
        });

        services.AddServicesApplication<ProvaContext>();

        services.ApplyMigrations();

        return services;
    }

    private static void ApplyMigrations(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProvaContext>();
        dbContext.Database.Migrate();
    }

}

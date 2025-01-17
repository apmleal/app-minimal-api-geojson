using NetTopologySuite.Features;
using Prova.Application;
using Scalar.AspNetCore;

namespace Prova.Api.Extensions;

public static class AppBuilder
{
    public static WebApplication UserServices(this WebApplication app)
    {

        app.UseHttpsRedirection();

        app.AppBuilderAplication();

        app.UseExceptionHandler();

        Feature.ComputeBoundingBoxWhenItIsMissing = true;

        app.MapOpenApi();
        app.ConfigureScalar();


        return app;
    }

    private static void ConfigureScalar(this WebApplication app)
    {
        app.MapScalarApiReference(options =>
        {
            options.WithTitle("Prova de Avaliação Técnica");
        });
    }
}

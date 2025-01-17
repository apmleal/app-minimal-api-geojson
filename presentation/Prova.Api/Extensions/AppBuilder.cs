using NetTopologySuite.Features;
using Prova.Application;

namespace Prova.Api.Extensions;

public static class AppBuilder
{
    public static WebApplication UserServices(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.AppBuilderAplication();

        app.UseExceptionHandler();

        Feature.ComputeBoundingBoxWhenItIsMissing = true;

        return app;
    }
}

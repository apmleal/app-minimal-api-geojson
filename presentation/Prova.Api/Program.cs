using Prova.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();

var app = builder.Build();


app.UserServices();

app.UseHttpsRedirection();


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

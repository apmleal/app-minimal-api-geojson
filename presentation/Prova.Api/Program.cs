using Prova.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UserServices();

app.UseHttpsRedirection();

app.Run();

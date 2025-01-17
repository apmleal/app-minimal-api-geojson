using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Prova.Application.Core.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";

        var exceptionDetalhe = exception switch
        {
            ValidationAppExceptions validationAppExceptions => (Detalhe: validationAppExceptions.Errors, StatusCode: StatusCodes.Status400BadRequest),
            _ => (Detalhe: (object)exception.Message, StatusCode: StatusCodes.Status500InternalServerError)
        };

        httpContext.Response.StatusCode = exceptionDetalhe.StatusCode;

        if (exception is ValidationAppExceptions validation)
        {
            await httpContext.Response.WriteAsJsonAsync(new { validation.Errors }, cancellationToken: cancellationToken);
            return true;
        }

        await httpContext.Response.WriteAsJsonAsync(new
        {
            Title = "Erro Interno",
            ProblemDetailsContext = exceptionDetalhe.Detalhe,
            Status = exceptionDetalhe.StatusCode,
            Type = exception.GetType().Name
        }, cancellationToken: cancellationToken);

        return false;
    }
}

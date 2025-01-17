using FluentValidation;
using MediatR;
using Prova.Application.Core.Notifications;

namespace Prova.Application.Core.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResponseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errosDictionary = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null);

        if (errosDictionary.Any())
        {
            foreach (var error in errosDictionary)
            {

                NotificationContext.Instance.AddNotification(new Notification(error.PropertyName, error.ErrorMessage));
            }

            var notificarions = NotificationContext.Instance.ToDictionary;

            var resp = new ResponseCommand();
            resp.AddErros(notificarions);

            return (resp as TResponse)!;
        }

        return await next();
    }
}

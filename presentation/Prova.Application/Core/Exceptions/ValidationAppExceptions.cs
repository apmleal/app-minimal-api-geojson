namespace Prova.Application.Core.Exceptions;

public class ValidationAppExceptions : Exception
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public ValidationAppExceptions(IReadOnlyDictionary<string, string[]> errors)
        : base("One or more validation failures have occurred.") => Errors = errors;
}

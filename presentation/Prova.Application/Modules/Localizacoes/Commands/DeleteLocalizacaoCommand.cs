using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prova.Application.Core.Behaviors;
using Prova.Domain;

namespace Prova.Application.Modules.Localizacoes.Commands;

public record DeleteLocalizacaoCommand(Guid Id) : IRequest<ResponseCommand>
{
}


public record DeleteLocalizacaoCommandHandler<TContext>(TContext context) : IRequestHandler<DeleteLocalizacaoCommand, ResponseCommand>
    where TContext : DbContext
{
    private readonly TContext _context = context;

    public async Task<ResponseCommand> Handle(DeleteLocalizacaoCommand request, CancellationToken cancellationToken)
    {
        var dbSet = _context.Set<Localizacao>();

        var localizacao = await dbSet.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (localizacao is null)
        {
            var resp = new ResponseCommand();
            resp.AddErros("localizacao", "localização não encontrada");
            return resp;
        }

        dbSet.Remove(localizacao);

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseCommand(localizacao.Id);
    }
}

public class DeleteLocalizacaoCommandValidator : AbstractValidator<DeleteLocalizacaoCommand>
{
    public DeleteLocalizacaoCommandValidator()
    {
        ValidateId();
    }

    public void ValidateId()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O identificador da localização é obrigatório");
    }
}


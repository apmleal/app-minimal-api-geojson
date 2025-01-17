using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Prova.Application.Core.Behaviors;
using Prova.Domain;

namespace Prova.Application.Modules.Localizacoes.Commands;

public record UpdateLocalizacaoCommand(Guid Id, string? Nome, int Categoria, double Longitude, double Latitude) : IRequest<ResponseCommand>
{
}


public record UpdateLocalizacaoCommandHandler<TContext>(TContext context) : IRequestHandler<UpdateLocalizacaoCommand, ResponseCommand>
    where TContext : DbContext
{
    private readonly TContext _context = context;

    public async Task<ResponseCommand> Handle(UpdateLocalizacaoCommand request, CancellationToken cancellationToken)
    {
        var dbSet = _context.Set<Localizacao>();

        var localizacao = await dbSet.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (localizacao is null)
        {
            var resp = new ResponseCommand();
            resp.AddErros("localizacao", "localização não encontrada");
            return resp;
        }


        var srid = 4326;
        var geometryFactory = new GeometryFactory();
        var point = geometryFactory.CreatePoint(new Coordinate(request.Longitude, request.Latitude));
        point.SRID = srid;

        localizacao.AlterarDados(request.Nome!, request.Categoria, point);

        dbSet.Update(localizacao);

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseCommand(localizacao.Id);
    }
}

public class UpdateLocalizacaoCommandValidator : AbstractValidator<UpdateLocalizacaoCommand>
{
    public UpdateLocalizacaoCommandValidator()
    {
        ValidateId();
        ValidateNome();
        ValidateCategoria();
        ValidateLongitude();
        ValidateLatitude();
    }

    public void ValidateId()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O identificador da localização é obrigatório");
    }

    public void ValidateNome()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório");
    }

    public void ValidateCategoria()
    {
        RuleFor(x => x.Categoria)
            .NotEmpty().WithMessage("Categoria é obrigatório");
    }

    public void ValidateLongitude()
    {
        RuleFor(x => x.Longitude)
            .NotEmpty().WithMessage("Longitude é obrigatório");

        RuleFor(x => x.Longitude)
            .Must(BeValidLongitude).WithMessage("Longitude deve estar entre -180 e 180 graus");
    }

    public void ValidateLatitude()
    {
        RuleFor(x => x.Latitude)
            .NotEmpty().WithMessage("Latitude é obrigatório");


        RuleFor(x => x.Latitude)
            .Must(BeValidLatitude).WithMessage("Latitude deve estar entre -90 e 90 graus");
    }

    private bool BeValidLongitude(double value)
    {
        return value >= -180 && value <= 180;
    }

    private bool BeValidLatitude(double value)
    {
        return value >= -90 && value <= 90;
    }
}


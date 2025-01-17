using NetTopologySuite.Geometries;

namespace Prova.Domain;

public class Localizacao
{
    public Guid Id { get; private set; } 
    public string Nome { get; private set; } = null!;
    public int Categoria { get; private set; } 
    public Point Local { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected Localizacao() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Localizacao(string nome, int categoria, Point local)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Categoria = categoria;
        Local = local;
    }

    public void AlterarDados(string nome, int categoria, Point local)
    {
        Nome = nome;
        Categoria = categoria;
        Local = local;
    }
}
using System.Reflection;

namespace Prova.Domain;

public sealed record Categoria(int Value, string Name)
{
    public static readonly Categoria Farmacia = new(1, "Farmácia");
    public static readonly Categoria Restaurante = new(2, "Restaurante");
    public static readonly Categoria Hospital = new(3, "Hospital");
    public static readonly Categoria Outro = new(4, "Outro");

    public static readonly List<Categoria> AllCategories =
    [
        Farmacia,
        Restaurante,
        Hospital,
        Outro
    ];

    public static Categoria FromValue(int value)
    {
        return value switch
        {
            1 => Farmacia,
            2 => Restaurante,
            3 => Hospital,
            4 => Outro,
            _ => throw new ArgumentException($"Invalid category value: {value}", nameof(value))
        };
    }

}

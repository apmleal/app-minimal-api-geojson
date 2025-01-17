using System.Collections;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Prova.Application.Core.Behaviors;

public class ResponseCommand
{
    public ResponseCommand() { }

    public ResponseCommand(object value)
    {
        Value = value;
        Erros = new Dictionary<string, string[]>(); // Initialize Erros to an empty dictionary
    }

    public void AddErros(IReadOnlyDictionary<string, string[]> erros)
    {
        Erros = erros;
        Value = default!; // Initialize Value to its default value
    }

    public void AddErros(string key, string message)
    {
        AddValueToExistingKey(key, message);
        Value = default!; // Initialize Value to its default value
    }

    private void AddValueToExistingKey(string key, string valueToAdd)
    {
        Erros ??= new Dictionary<string, string[]>(); // Initialize Erros to an empty dictionary
        var mutableErros = new Dictionary<string, string[]>(Erros);

        if (mutableErros.TryGetValue(key, out string[] existingValues))
        {
            int newValueCount = existingValues.Length + 1;
            string[] newValues = new string[newValueCount];
            Array.Copy(existingValues, newValues, existingValues.Length);
            newValues[newValueCount - 1] = valueToAdd;
            mutableErros[key] = newValues;
        }
        else
        {
            mutableErros[key] = new string[] { valueToAdd };
        }

        Erros = mutableErros;
    }

    public IReadOnlyDictionary<string, string[]> Erros { get; set; }

    public object Value { get; set; }

    public bool IsValid => Erros.Count == 0;
}

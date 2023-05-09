using System.Diagnostics.CodeAnalysis;

namespace FightWasteConsole.CommandArguments;

[ExcludeFromCodeCoverage]
public class Argument
{
    public string? ArgumentName { get; set; }
    public List<string>? ArgumentValues { get; set; }
}

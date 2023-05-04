using FightWasteConsole.CommandArguments;

namespace FightWasteConsole.Commands;

public interface ICommand
{
    IEnumerable<string> Aliases { get; }
    void Execute(List<Argument> arguments);
}

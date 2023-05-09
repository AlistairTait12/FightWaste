using FightWasteConsole.CommandArguments;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.Repositories;

namespace FightWasteConsole.Commands;

public class FindMealCommand : ICommand
{
    private readonly IMealRepository _mealRepository;
    private readonly IConsoleWrapper _consoleWrapper;
    private readonly List<string> _aliasesForNameArg = new() { "name", "n" };

    public FindMealCommand(IMealRepository mealRepository, IConsoleWrapper consoleWrapper)
    {
        _mealRepository = mealRepository;
        _consoleWrapper = consoleWrapper;
    }

    public IEnumerable<string> Aliases => throw new NotImplementedException();

    public void Execute(List<Argument> arguments)
    {
        var nameArg = arguments.FirstOrDefault(arg => _aliasesForNameArg.Contains(arg.ArgumentName));

        if (nameArg is null)
        {
            _consoleWrapper.Warn($"Argument `{}`");
        }
    }
}

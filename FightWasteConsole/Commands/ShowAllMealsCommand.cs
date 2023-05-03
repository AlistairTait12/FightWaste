using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.DataAccess;
using FightWasteConsole.Models;
using FightWasteConsole.Repositories;

namespace FightWasteConsole.Commands;

public class ShowAllMealsCommand : ICommand
{
    private readonly IMealRepository _mealRepository;
    private readonly IConsoleWrapper _consoleWrapper;

    public ShowAllMealsCommand(IMealRepository mealRepository, IConsoleWrapper consoleWrapper)
    {
        _mealRepository = mealRepository;
        _consoleWrapper = consoleWrapper;
    }

    public IEnumerable<string> Aliases => throw new NotImplementedException();

    public void Execute()
    {
        throw new NotImplementedException();
    }
}

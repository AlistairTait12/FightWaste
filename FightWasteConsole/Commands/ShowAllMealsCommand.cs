using FightWasteConsole.CommandArguments;
using FightWasteConsole.ConsoleWrapper;
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

    public IEnumerable<string> Aliases => new List<string>
    {
        CommandStringConstants.SHOWALL,
        CommandStringConstants.SHOWALLMEALS,
        CommandStringConstants.SA
    };

    public void Execute(List<Argument> arguments = null!)
    {
        _consoleWrapper.Write(GetOutput());
    }

    private string GetOutput() => 
        string.Join("\r\n", _mealRepository.GetAll().Select(meal => meal.Name));
}

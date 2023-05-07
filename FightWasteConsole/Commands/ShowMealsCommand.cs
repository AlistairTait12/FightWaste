using FightWasteConsole.CommandArguments;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.Models;
using FightWasteConsole.Repositories;
using System.Text;

namespace FightWasteConsole.Commands;

public class ShowMealsCommand : ICommand
{
    private readonly IMealRepository _mealRepository;
    private readonly IConsoleWrapper _consoleWrapper;

    public ShowMealsCommand(IMealRepository mealRepository, IConsoleWrapper consoleWrapper)
    {
        _mealRepository = mealRepository;
        _consoleWrapper = consoleWrapper;
    }

    public IEnumerable<string> Aliases
        => new List<string> { CommandStringConstants.SHOWMEALS };

    public void Execute(List<Argument> arguments)
    {
        var argAliases = new List<string>
        {
            "number",
            "n"
        };

        var numberOfMeals = arguments.FirstOrDefault(arg => argAliases.Contains(arg.ArgumentName)).ArgumentValues.FirstOrDefault();

        _consoleWrapper.Write(GetMealsOutput(int.Parse(numberOfMeals)));
    }

    private string GetMealsOutput(int numberOfMeals)
    {
        var sb = new StringBuilder();
        var allMeals = _mealRepository.GetAll();

        if (numberOfMeals > allMeals.Count())
        {
            _consoleWrapper.Warn($"Showing maximum ({allMeals.Count()}) meals in database");
        }
        
        var requestedMeals = allMeals.Take(numberOfMeals).ToList();

        requestedMeals.ForEach(meal => sb.AppendLine(meal.Name));

        return sb.ToString();
    }
}

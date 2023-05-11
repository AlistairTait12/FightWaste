using FightWasteConsole.CommandArguments;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.Models;
using FightWasteConsole.Output;
using FightWasteConsole.Repositories;

namespace FightWasteConsole.Commands;

public class FindMealCommand : ICommand
{
    private readonly IMealRepository _mealRepository;
    private readonly IConsoleWrapper _consoleWrapper;
    private readonly IModelCollectionOutputter<IngredientQuantityModel> _modelCollectionOutputter;
    private readonly List<string> _aliasesForNameArg = new() { "name", "n" };

    public FindMealCommand(IMealRepository mealRepository, IConsoleWrapper consoleWrapper,
        IModelCollectionOutputter<IngredientQuantityModel> modelCollectionOutputter)
    {
        _mealRepository = mealRepository;
        _consoleWrapper = consoleWrapper;
        _modelCollectionOutputter = modelCollectionOutputter;
    }

    public IEnumerable<string> Aliases => new List<string> { "findmeal" };

    public void Execute(List<Argument> arguments)
    {
        var nameArg = arguments.FirstOrDefault(arg => _aliasesForNameArg.Contains(arg.ArgumentName));

        if (nameArg is null)
        {
            _consoleWrapper.Warn($"Argument name not found, this command can accept {string.Join(" -", _aliasesForNameArg)}");
            return;
        }

        var meal = _mealRepository.GetMealByName(nameArg.ArgumentValues.FirstOrDefault());

        if (meal is null)
        {
            _consoleWrapper.Warn($"Meal of name `{nameArg.ArgumentValues.FirstOrDefault()}` not found, please check your input");
            return;
        }

        var output = _modelCollectionOutputter.GetListAsCollection(meal.Ingredients);

        _consoleWrapper.Write(output);
    }
}

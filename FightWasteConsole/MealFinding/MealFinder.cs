using FightWasteConsole.Models;
using FightWasteConsole.Repositories;
using System.ComponentModel.DataAnnotations;

namespace FightWasteConsole.MealFinding;

public class MealFinder : IMealFinder
{
    private readonly IRepository<MealModel> _repository;
    private readonly IConsoleWrapper _consoleWrapper;

    public MealFinder(IRepository<MealModel> repository, IConsoleWrapper consoleWrapper)
    {
        _repository = repository;
        _consoleWrapper = consoleWrapper;
    }

    public MealModel FindMealByName()
    {
        throw new NotImplementedException();
    }
}

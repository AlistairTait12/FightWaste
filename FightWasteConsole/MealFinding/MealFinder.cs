using FightWasteConsole.Models;
using FightWasteConsole.Repositories;

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
        var mealFound = false;
        var meal = new MealModel();

        while (!mealFound)
        {
            var mealToFind = _consoleWrapper.Read();
            meal = _repository
                .GetAll()
                .FirstOrDefault(m => m.Name == mealToFind);

            mealFound = meal != null;
            if (!mealFound)
            {
                _consoleWrapper.Warn($"Meal '{mealToFind}' not found, please enter another meal");
            }
        }

        _consoleWrapper.Confirm($"Meal '{meal.Name}' added");
        return meal;
    }
}

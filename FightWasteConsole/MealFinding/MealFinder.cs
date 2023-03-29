using FightWasteConsole.Models;
using FightWasteConsole.Repositories;

namespace FightWasteConsole.MealFinding;

public class MealFinder : IMealFinder
{
    private readonly IRepository<MealModel> _repository;

    public MealFinder(IRepository<MealModel> repository)
    {
        _repository = repository;
    }

    public MealModel FindMealByName(string name)
    {
        var meal = _repository.GetAll().FirstOrDefault(meal => meal.Name == name);

        return meal;
    }
}

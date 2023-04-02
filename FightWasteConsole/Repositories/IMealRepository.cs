using FightWasteConsole.Models;

namespace FightWasteConsole.Repositories;

public interface IMealRepository
{
    MealModel GetMealByName(string name);
}

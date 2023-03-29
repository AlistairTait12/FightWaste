using FightWasteConsole.Models;

namespace FightWasteConsole.MealFinding;

public interface IMealFinder
{
    MealModel FindMealByName(string name);
}

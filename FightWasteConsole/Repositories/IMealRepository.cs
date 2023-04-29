using FightWasteConsole.Models;

namespace FightWasteConsole.Repositories;

public interface IMealRepository : IRepository<MealModel>
{
    MealModel GetMealByName(string name);
}

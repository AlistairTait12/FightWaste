using FightWasteConsole.DataAccess;
using FightWasteConsole.Models;

namespace FightWasteConsole.Repositories;

public class MealRepository : ModelRepository<MealModel>,
    IRepository<MealModel>, IMealRepository
{
    public MealRepository(IDataAccess<MealModel> dataAccess) : base(dataAccess)
    {
    }

    public MealModel GetMealByName(string name)
        => _dataAccess.GetData().FirstOrDefault(model => model.Name == name)!;
}

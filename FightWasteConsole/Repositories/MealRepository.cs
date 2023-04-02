﻿using FightWasteConsole.DataAccess;
using FightWasteConsole.Models;

namespace FightWasteConsole.Repositories;

public class MealRepository : ModelRepository<MealModel>, IRepository<MealModel>
{
    public MealRepository(IDataAccess<MealModel> dataAccess) : base(dataAccess)
    {
    }

    public MealModel GetMealByName(string name)
    {
        throw new NotImplementedException();
    }
}

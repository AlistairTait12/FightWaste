using FightWasteConsole.Models;

namespace FightWasteConsole.Aggregation;

public interface IIngredientAggregator
{
    IEnumerable<IngredientQuantityModel> CombineIngredients(IEnumerable<IngredientQuantityModel> ingredients);
}

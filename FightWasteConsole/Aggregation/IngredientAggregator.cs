using FightWasteConsole.Models;

namespace FightWasteConsole.Aggregation;

public class IngredientAggregator : IIngredientAggregator
{
    public IEnumerable<IngredientQuantityModel> CombineIngredients(IEnumerable<IngredientQuantityModel> ingredients)
    {
        var uniqueIngredientNames = ingredients
            .Select(ingredient => ingredient.Name)
            .Distinct();

        var ingredientsToReturn = new List<IngredientQuantityModel>();

        uniqueIngredientNames.ToList().ForEach(name =>
        {
            var totalQuantity = ingredients
                .Where(ingredient => ingredient.Name == name)
                .Select(ingredient => ingredient.Quantity)
                .Sum();

            ingredientsToReturn.Add(new IngredientQuantityModel
            {
                Name = name,
                Quantity = totalQuantity,
                Unit = ingredients.First(a => a.Name == name).Unit
            });
        });

        return ingredientsToReturn;
    }
}

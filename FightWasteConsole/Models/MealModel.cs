namespace FightWasteConsole.Models;

public class MealModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<IngredientQuantityModel>? Ingredients { get; set; }
}

using FightWasteConsole.Aggregation;
using FightWasteConsole.MealFinding;
using FightWasteConsole.Models;
using FightWasteConsole.Output;

namespace FightWasteConsole.IngredientsListProcessing;

public class IngredientsListProcessor : IIngredientsListProcessor
{
    private readonly IModelCollectionOutputter<IngredientQuantityModel> _modelCollectionOutputter;
    private readonly IMealFinder _mealFinder;
    private readonly IIngredientAggregator _ingredientAggregator;
    private readonly IConsoleWrapper _consoleWrapper;

    public IngredientsListProcessor(
        IModelCollectionOutputter<IngredientQuantityModel> modelCollectionOutputter,
        IMealFinder mealFinder,
        IIngredientAggregator ingredientAggregator,
        IConsoleWrapper consoleWrapper)
    {
        _modelCollectionOutputter = modelCollectionOutputter;
        _mealFinder = mealFinder;
        _ingredientAggregator = ingredientAggregator;
        _consoleWrapper = consoleWrapper;
    }

    public void ProduceIngredientsList()
    {
        var allMeals = new List<MealModel>();

        // HACK: Ask 7 times for now, implement way of user breaking out in future
        _consoleWrapper.Write("Please enter your meals for the week");
        for (int i = 0; i < 7; i++)
        {
            allMeals.Add(_mealFinder.FindMealByName());
        }

        var allIngredients = allMeals.SelectMany(meal => meal.Ingredients);
        var combinedIngredients = _ingredientAggregator.CombineIngredients(allIngredients);
        var output = _modelCollectionOutputter.GetListAsCollection(combinedIngredients.ToList());

        Console.Write(output);
    }
}

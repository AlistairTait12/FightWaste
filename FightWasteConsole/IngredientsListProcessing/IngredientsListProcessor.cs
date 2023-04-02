using FightWasteConsole.Aggregation;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.FileWriter;
using FightWasteConsole.Models;
using FightWasteConsole.Output;

namespace FightWasteConsole.IngredientsListProcessing;

public class IngredientsListProcessor : IIngredientsListProcessor
{
    private readonly IModelCollectionOutputter<IngredientQuantityModel> _modelCollectionOutputter;
    private readonly IIngredientAggregator _ingredientAggregator;
    private readonly IConsoleWrapper _consoleWrapper;
    private readonly IFileWriter _writer;

    public IngredientsListProcessor(
        IModelCollectionOutputter<IngredientQuantityModel> modelCollectionOutputter,
        IIngredientAggregator ingredientAggregator,
        IConsoleWrapper consoleWrapper,
        IFileWriter writer)
    {
        _modelCollectionOutputter = modelCollectionOutputter;
        _ingredientAggregator = ingredientAggregator;
        _consoleWrapper = consoleWrapper;
        _writer = writer;
    }

    public void ProduceIngredientsList()
    {
        var allMeals = new List<MealModel>();

        // HACK: Ask 7 times for now, implement way of user breaking out in future
        _consoleWrapper.Write("Please enter your meals for the week");
        for (int i = 0; i < 7; i++)
        {
        }

        var allIngredients = allMeals.SelectMany(meal => meal.Ingredients);
        var combinedIngredients = _ingredientAggregator.CombineIngredients(allIngredients);
        var output = _modelCollectionOutputter.GetListAsCollection(combinedIngredients.ToList());

        Console.Write(output);
        _writer.WriteIngredientsToFile(combinedIngredients);
    }
}

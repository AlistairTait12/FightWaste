using FightWasteConsole.Aggregation;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.FileWriter;
using FightWasteConsole.Models;
using FightWasteConsole.Output;
using FightWasteConsole.Repositories;

namespace FightWasteConsole.IngredientsListProcessing;

public class IngredientsListProcessor : IIngredientsListProcessor
{
    private readonly IModelCollectionOutputter<IngredientQuantityModel> _modelCollectionOutputter;
    private readonly IIngredientAggregator _ingredientAggregator;
    private readonly IConsoleWrapper _consoleWrapper;
    private readonly IFileWriter _writer;
    private readonly IMealRepository _repository;

    public IngredientsListProcessor(
        IModelCollectionOutputter<IngredientQuantityModel> modelCollectionOutputter,
        IIngredientAggregator ingredientAggregator,
        IConsoleWrapper consoleWrapper,
        IFileWriter writer,
        IMealRepository repository)
    {
        _modelCollectionOutputter = modelCollectionOutputter;
        _ingredientAggregator = ingredientAggregator;
        _consoleWrapper = consoleWrapper;
        _writer = writer;
        _repository = repository;
    }

    public void ProduceIngredientsList()
    {
        var allMeals = new List<MealModel>();

        _consoleWrapper.Write("Please enter your meals for the week");
        var userFinished = false;

        while (!userFinished)
        {
            var userResponse = _consoleWrapper.Read();

            // TODO: Cover case insensitivity of `end` in a unit test
            if(string.Equals("End", userResponse, StringComparison.InvariantCultureIgnoreCase))
            {
                _consoleWrapper.Write("Meal selection complete, compiling list of ingredients");
                break;
            }

            if (string.Equals("showall", userResponse, StringComparison.InvariantCultureIgnoreCase))
            {
                var meals = string.Join("\r\n", _repository.GetAll().Select(meal => meal.Name));
                _consoleWrapper.Write(meals);
                continue;
            }

            var mealToAdd = _repository.GetMealByName(userResponse);

            if (mealToAdd != null)
            {
                _consoleWrapper.Confirm($"Meal '{mealToAdd.Name}' added");
                allMeals.Add(mealToAdd);
            }
            else
            {
                _consoleWrapper.Warn($"Meal '{userResponse}' not found, please enter another meal");
            }
        }

        var allIngredients = allMeals.SelectMany(meal => meal.Ingredients!);
        var combinedIngredients = _ingredientAggregator.CombineIngredients(allIngredients);
        var output = _modelCollectionOutputter.GetListAsCollection(combinedIngredients.ToList());

        Console.Write(output);
        _writer.WriteIngredientsToFile(combinedIngredients);
    }
}

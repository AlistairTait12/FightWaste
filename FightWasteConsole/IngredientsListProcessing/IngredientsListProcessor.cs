using FightWasteConsole.Aggregation;
using FightWasteConsole.MealFinding;
using FightWasteConsole.Models;
using FightWasteConsole.Output;
using FightWasteConsole.Repositories;

namespace FightWasteConsole.IngredientsListProcessing;

public class IngredientsListProcessor
{
    private readonly IRepository<MealModel> _mealRepository;
    private readonly IModelCollectionOutputter<IngredientQuantityModel> _modelCollectionOutputter;
    private readonly IMealFinder _mealFinder;
    private readonly IIngredientAggregator _ingredientAggregator;

    public IngredientsListProcessor(IRepository<MealModel> mealRepository,
        IModelCollectionOutputter<IngredientQuantityModel> modelCollectionOutputter,
        IMealFinder mealFinder,
        IIngredientAggregator ingredientAggregator)
    {
        _mealRepository = mealRepository;
        _modelCollectionOutputter = modelCollectionOutputter;
        _mealFinder = mealFinder;
        _ingredientAggregator = ingredientAggregator;
    }

    public void GetIngredientsList()
    {
        var allMeals = new List<MealModel>();

        // HACK: Ask 7 times for now, implement way of user breaking out in future
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

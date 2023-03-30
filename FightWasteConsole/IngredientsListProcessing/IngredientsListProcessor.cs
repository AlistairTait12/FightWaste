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

    public IngredientsListProcessor(IRepository<MealModel> mealRepository,
        IModelCollectionOutputter<IngredientQuantityModel> modelCollectionOutputter,
        IMealFinder mealFinder)
    {
        _mealRepository = mealRepository;
        _modelCollectionOutputter = modelCollectionOutputter;
        _mealFinder = mealFinder;
    }

    public void GetIngredientsList()
    {
        // TODO: Declare empty mealModel list
        var allMeals = new List<MealModel>();

        // TODO: Ask the user to enter a meal with a MealFinder.FindMeal(userInput);
        // HACK: Ask 7 times for now, implement way of user breaking out in future
        for (int i = 0; i < 7; i++)
        {
            allMeals.Add(_mealFinder.FindMealByName());
        }

        // TODO: Smush together ingredient quantities with an ingredient
        //       quantity aggregator

        // TODO: Sort the list alphabetically by ingredient

        // TODO: Feed the aggregated list into IModelCollectionOutputter
    }
}

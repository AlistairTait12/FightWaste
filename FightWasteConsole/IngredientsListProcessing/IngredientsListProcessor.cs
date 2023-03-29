using FightWasteConsole.Models;
using FightWasteConsole.Output;
using FightWasteConsole.Repositories;

namespace FightWasteConsole.IngredientsListProcessing;

public class IngredientsListProcessor
{
    private readonly IRepository<MealModel> _mealRepository;
    private readonly IModelCollectionOutputter<IngredientQuantityModel> _modelCollectionOutputter;

    public IngredientsListProcessor(IRepository<MealModel> mealRepository,
        IModelCollectionOutputter<IngredientQuantityModel> modelCollectionOutputter)
    {
        _mealRepository = mealRepository;
        _modelCollectionOutputter = modelCollectionOutputter;
    }

    public void GetIngredientsList()
    {
        // TODO: Declare empty mealModel list
        var allMeals = new List<MealModel>();

        // TODO: Ask the user to enter a meal with a MealFinder.FindMeal(userInput);

        // TODO: Check it exists

        // TODO: Add it to the list ^^

        // TODO: Handle when user finished entering meals

        // TODO: Smush together ingredient quantities with an ingredient
        //       quantity aggregator

        // TODO: Sort the list alphabetically by ingredient

        // TODO: Feed the aggregated list into IModelCollectionOutputter
    }
}

using FightWasteConsole.Aggregation;
using FightWasteConsole.DataAccess;
using FightWasteConsole.FileWriter;
using FightWasteConsole.IngredientsListProcessing;
using FightWasteConsole.MealFinding;
using FightWasteConsole.Models;
using FightWasteConsole.Output;
using FightWasteConsole.Repositories;
using Microsoft.Extensions.DependencyInjection;

// HACK: Get this in an appsettings file
var filePath = "C:\\Source\\FightWaste\\Meals.json";
var ingredientsPath = "C:\\Source\\FightWaste\\Output\\Ingredients\\";

var serviceCollection = new ServiceCollection();

serviceCollection
    .AddTransient<IIngredientAggregator, IngredientAggregator>()
    .AddTransient<IDataAccess<MealModel>>(services =>
    {
        return new JsonDataAccess<MealModel>(filePath);
    })
    .AddTransient<IIngredientsListProcessor, IngredientsListProcessor>()
    .AddTransient<IConsoleWrapper, ConsoleWrapper>()
    .AddTransient<IMealFinder, MealFinder>()
    .AddTransient<IModelCollectionOutputter<IngredientQuantityModel>, ModelTableOutputter<IngredientQuantityModel>>()
    .AddTransient<IRepository<MealModel>, ModelRepository<MealModel>>()
    .AddTransient<IFileWriter>(services =>
    {
        // TODO, using IOptions would allow avoidance of newing up ModelTableOutputter in here
        return new TableFileWriter(ingredientsPath, new ModelTableOutputter<IngredientQuantityModel>());
    });

var processor = serviceCollection
    .BuildServiceProvider()
    .GetRequiredService<IIngredientsListProcessor>();

processor.ProduceIngredientsList();

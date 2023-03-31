using FightWasteConsole.Aggregation;
using FightWasteConsole.DataAccess;
using FightWasteConsole.IngredientsListProcessing;
using FightWasteConsole.MealFinding;
using FightWasteConsole.Models;
using FightWasteConsole.Output;
using FightWasteConsole.Repositories;
using Microsoft.Extensions.DependencyInjection;

var filePath = "";

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
    .AddTransient<IRepository<MealModel>, ModelRepository<MealModel>>();

var processor = serviceCollection
    .BuildServiceProvider()
    .GetRequiredService<IIngredientsListProcessor>();

processor.ProduceIngredientsList();

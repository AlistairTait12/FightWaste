using FightWasteConsole;
using FightWasteConsole.Aggregation;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.DataAccess;
using FightWasteConsole.FileWriter;
using FightWasteConsole.IngredientsListProcessing;
using FightWasteConsole.Models;
using FightWasteConsole.Options;
using FightWasteConsole.Output;
using FightWasteConsole.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, configuration) =>
    {
        configuration.Sources.Clear();
        IHostEnvironment env = hostContext.HostingEnvironment;
        configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        var configurationRoot = hostContext.Configuration;
        services.Configure<FightWasteOptions>(configurationRoot.GetSection(nameof(FightWasteOptions)));

        services.AddHostedService<FightWasteHost>();
        services.AddTransient<IIngredientsListProcessor, IngredientsListProcessor>()
            .AddTransient<IConsoleWrapper, ConsoleWrapper>()
            .AddTransient<IModelCollectionOutputter<IngredientQuantityModel>, ModelTableOutputter<IngredientQuantityModel>>()
            .AddTransient<IMealRepository, MealRepository>()
            .AddTransient<IFileWriter, TableFileWriter>()
            .AddTransient<IIngredientAggregator, IngredientAggregator>()
            .AddTransient<IDataAccess<MealModel>, JsonDataAccess<MealModel>>();
    }).Build();

host.Run();
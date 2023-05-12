using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.IngredientsListProcessing;
using FightWasteConsole.Models;
using FightWasteConsole.Output;
using FightWasteConsole.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FightWasteConsole.Commands;

internal class CommandContainer
{
    private readonly IServiceProvider _serviceProvider;

    internal CommandContainer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    internal List<ICommand> GetAllCommands() => new()
    {
        new NewIngredientsListCommand(_serviceProvider.GetRequiredService<IIngredientsListProcessor>()),
        new ShowAllMealsCommand(_serviceProvider.GetRequiredService<IMealRepository>(),
            _serviceProvider.GetRequiredService<IConsoleWrapper>()),
        new ShowMealsCommand(_serviceProvider.GetRequiredService<IMealRepository>(),
            _serviceProvider.GetRequiredService<IConsoleWrapper>()),
        new FindMealCommand(_serviceProvider.GetRequiredService<IMealRepository>(),
            _serviceProvider.GetRequiredService<IConsoleWrapper>(),
            _serviceProvider.GetRequiredService<IModelCollectionOutputter<IngredientQuantityModel>>())
    };
}

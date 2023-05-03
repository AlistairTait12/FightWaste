using FightWasteConsole.IngredientsListProcessing;
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
        new NewIngredientsListCommand(_serviceProvider.GetRequiredService<IIngredientsListProcessor>())
    };
}

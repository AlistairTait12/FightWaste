using FightWasteConsole.IngredientsListProcessing;
using Microsoft.Extensions.Hosting;

namespace FightWasteConsole;

public class FightWasteHost : IHostedService
{
    private IIngredientsListProcessor _processor;

    public FightWasteHost(IIngredientsListProcessor processor)
    {
        _processor = processor;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _processor.ProduceIngredientsList();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

using FightWasteConsole.Commands;
using Microsoft.Extensions.Hosting;

namespace FightWasteConsole;

public class FightWasteHost : IHostedService
{
    private ICommandListener _commandListener;

    public FightWasteHost(ICommandListener commandListener)
    {
        _commandListener = commandListener;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _commandListener.Listen();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

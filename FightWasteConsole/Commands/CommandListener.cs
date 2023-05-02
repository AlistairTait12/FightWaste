using FightWasteConsole.ConsoleWrapper;

namespace FightWasteConsole.Commands;

public class CommandListener : ICommandListener
{
    private readonly IEnumerable<ICommand> _commands;
    private readonly IConsoleWrapper _consoleWrapper;

    public CommandListener(IEnumerable<ICommand> commands, IConsoleWrapper consoleWrapper)
    {
        _commands = commands;
        _consoleWrapper = consoleWrapper;
    }

    public void Listen()
    {
        throw new NotImplementedException();
    }
}

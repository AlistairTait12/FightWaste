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
        var commandName = _consoleWrapper.Read();

        while (!string.Equals(commandName, "end",
            StringComparison.InvariantCultureIgnoreCase))
        {
            var selectedCommand = _commands.FirstOrDefault(command => command.Aliases
                .Select(alias => alias.ToLower())
                .Contains(commandName.ToLower()));

            if (selectedCommand != null)
            {
                selectedCommand.Execute();
            }
            else
            {
                _consoleWrapper.Warn($"command `{commandName}` not found");
            }

            commandName = _consoleWrapper.Read();
        }
    }
}

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
        string userInput;

        while (!string.Equals(userInput = _consoleWrapper.Read(), "exit",
            StringComparison.InvariantCultureIgnoreCase))
        {
            var selectedCommand = GetCommand(userInput);

            if (selectedCommand is not null)
            {
                selectedCommand.Execute();
            }
            else
            {
                _consoleWrapper.Warn($"command `{userInput}` not found");
            }
        }
    }

    private ICommand GetCommand(string commandName) =>
        _commands.FirstOrDefault(command => command.Aliases
                .Select(alias => alias.ToLower())
                .Contains(commandName.ToLower()))!;
}

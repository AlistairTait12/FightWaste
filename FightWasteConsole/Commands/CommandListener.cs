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
            var target = ExtractCommandString(userInput);

            var selectedCommand = GetCommand(target);

            if (selectedCommand is not null)
            {
                selectedCommand.Execute(null!);
            }
            else
            {
                _consoleWrapper.Warn($"command `{userInput}` not found");
            }
        }
    }

    private string ExtractCommandString(string fullString)
    {
        return fullString.Split(' ').ElementAt(0);
    }

    private ICommand GetCommand(string commandName) =>
        _commands.FirstOrDefault(command => command.Aliases
                .Select(alias => alias.ToLower())
                .Contains(commandName.ToLower()))!;
}

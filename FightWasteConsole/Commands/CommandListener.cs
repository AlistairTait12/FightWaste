﻿using FightWasteConsole.CommandArguments;
using FightWasteConsole.ConsoleWrapper;
using System.Diagnostics;

namespace FightWasteConsole.Commands;

public class CommandListener : ICommandListener
{
    private readonly IEnumerable<ICommand> _commands;
    private readonly IConsoleWrapper _consoleWrapper;
    private readonly IArgumentListBuilder _argumentBuilder;

    public CommandListener(IEnumerable<ICommand> commands, IConsoleWrapper consoleWrapper,
        IArgumentListBuilder argumentBuilder)
    {
        _commands = commands;
        _consoleWrapper = consoleWrapper;
        _argumentBuilder = argumentBuilder;
    }

    public void Listen()
    {
        string userInput;

        while (!string.Equals(userInput = _consoleWrapper.Read(), "exit",
            StringComparison.InvariantCultureIgnoreCase))
        {
            var target = ExtractCommandString(userInput);
            var arguments = _argumentBuilder.Build(userInput);

            var selectedCommand = GetCommand(target);

            if (selectedCommand is not null)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                selectedCommand.Execute(arguments);
                _consoleWrapper.Confirm($"Command {target} ran in {stopWatch.ElapsedMilliseconds}ms\r\n");
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

    private static string ExtractCommandString(string fullString)
    {
        return fullString.Split(' ').ElementAt(0);
    }
}

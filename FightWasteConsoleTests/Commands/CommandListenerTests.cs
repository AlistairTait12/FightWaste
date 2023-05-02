using FightWasteConsole.Commands;
using FightWasteConsole.ConsoleWrapper;
using System.Diagnostics.CodeAnalysis;

namespace FightWasteConsoleTests.Commands;

[ExcludeFromCodeCoverage]
[TestFixture]
public class CommandListenerTests
{
    private CommandListener _commandListener;
    private List<ICommand> _commands;
    private ICommand _fakeCommand;
    private IConsoleWrapper _consoleWrapper;

    [SetUp]
    public void SetUp()
    {
        _commands = new List<ICommand>();
        _consoleWrapper = A.Fake<IConsoleWrapper>();

        _commandListener = new CommandListener(_commands, _consoleWrapper);
        _fakeCommand = A.Fake<ICommand>();

        A.CallTo(() => _fakeCommand.Aliases).Returns(new List<string> { "fake", "f" });

        _commands.Add(_fakeCommand);
    }

    [Test]
    public void ListenWhenCommandExistsExecutesCommandAndListensAgain()
    {
        // Arrange
        A.CallTo(() => _consoleWrapper.Read()).Returns("end");
        A.CallTo(() => _consoleWrapper.Read()).ReturnsNextFromSequence("fake");

        // Act
        _commandListener.Listen();

        // Assert
        A.CallTo(() => _fakeCommand.Execute()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _consoleWrapper.Read()).MustHaveHappenedTwiceExactly();
    }

    [Test]
    public void ListenWhenCommandDoesNotExistDoesNotExecuteInformsUserAndListensAgain()
    {
        // Arrange
        A.CallTo(() => _consoleWrapper.Read()).Returns("end");
        A.CallTo(() => _consoleWrapper.Read()).ReturnsNextFromSequence("lawrence");

        // Act
        _commandListener.Listen();

        // Assert
        A.CallTo(() => _fakeCommand.Execute()).MustNotHaveHappened();
        A.CallTo(() => _consoleWrapper.Warn("command `lawrence` not found")).MustHaveHappenedOnceExactly();
        A.CallTo(() => _consoleWrapper.Read()).MustHaveHappenedTwiceExactly();
    }
}

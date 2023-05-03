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
        A.CallTo(() => _consoleWrapper.Read()).Returns("exit");
        A.CallTo(() => _consoleWrapper.Read()).ReturnsNextFromSequence("fake");

        // Act
        _commandListener.Listen();

        // Assert
        A.CallTo(() => _fakeCommand.Execute()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _consoleWrapper.Read()).MustHaveHappenedTwiceExactly();
    }

    [Test]
    public void ListenHandlesCallToSameCommandWhenItHasMultipleAlisases()
    {
        // Arrange
        A.CallTo(() => _consoleWrapper.Read()).Returns("exit");
        A.CallTo(() => _consoleWrapper.Read()).ReturnsNextFromSequence("f");

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
        A.CallTo(() => _consoleWrapper.Read()).Returns("exit");
        A.CallTo(() => _consoleWrapper.Read()).ReturnsNextFromSequence("lawrence");

        // Act
        _commandListener.Listen();

        // Assert
        A.CallTo(() => _fakeCommand.Execute()).MustNotHaveHappened();
        A.CallTo(() => _consoleWrapper.Warn("command `lawrence` not found")).MustHaveHappenedOnceExactly();
        A.CallTo(() => _consoleWrapper.Read()).MustHaveHappenedTwiceExactly();
    }

    [Test]
    public void ListenWhenMultipleCommandsExistCallsTheCorrectCommand()
    {
        // Arrange
        var fakeCommands = GetMultipleCommands();

        A.CallTo(() => _consoleWrapper.Read()).Returns("exit");
        A.CallTo(() => _consoleWrapper.Read()).ReturnsNextFromSequence("dt");

        _commandListener = new CommandListener(fakeCommands, _consoleWrapper);

        // Act
        _commandListener.Listen();

        // Assert
        A.CallTo(() => fakeCommands.ElementAt(1).Execute()).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void ListenHandlesMultipleCommandExecutionsUntilUserEntersEnd()
    {
        // Arrange
        var fakeCommands = GetMultipleCommands();

        A.CallTo(() => _consoleWrapper.Read()).Returns("exit");
        A.CallTo(() => _consoleWrapper.Read()).ReturnsNextFromSequence("stvn", "crahh", "dothing");

        _commandListener = new CommandListener(fakeCommands, _consoleWrapper);

        // Act
        _commandListener.Listen();

        // Assert
        A.CallTo(() => fakeCommands.ElementAt(3).Execute()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _consoleWrapper.Warn("command `crahh` not found")).MustHaveHappenedOnceExactly();
        A.CallTo(() => fakeCommands.ElementAt(1).Execute()).MustHaveHappenedOnceExactly();
    }

    private static List<ICommand> GetMultipleCommands()
    {
        var fakeCommandOne = A.Fake<ICommand>();
        A.CallTo(() => fakeCommandOne.Aliases).Returns(new List<string> { "bob", "b" });
        var fakeCommandTwo = A.Fake<ICommand>();
        A.CallTo(() => fakeCommandTwo.Aliases).Returns(new List<string> { "dothing", "dt" });
        var fakeCommandThree = A.Fake<ICommand>();
        A.CallTo(() => fakeCommandThree.Aliases).Returns(new List<string> { "foo", "f", "jack" });
        var fakeCommandFour = A.Fake<ICommand>();
        A.CallTo(() => fakeCommandFour.Aliases).Returns(new List<string> { "steven", "stvn" });

        return new()
        {
            fakeCommandOne,
            fakeCommandTwo,
            fakeCommandThree,
            fakeCommandFour
        };
    }
}

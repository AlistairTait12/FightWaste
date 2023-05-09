using FightWasteConsole.CommandArguments;
using FightWasteConsole.Commands;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.Models;
using FightWasteConsole.Repositories;
using NUnit.Framework.Internal;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FightWasteConsoleTests.Commands;

[ExcludeFromCodeCoverage]
[TestFixture]
public class FindMealCommandTests
{
    private IMealRepository _mealRepository;
    private IConsoleWrapper _consoleWrapper;
    private FindMealCommand _findMealCommand;

    [SetUp]
    public void SetUp()
    {
        _mealRepository = A.Fake<IMealRepository>();
        A.CallTo(() => _mealRepository.GetAll()).Returns(GetMealModels());
        _consoleWrapper = A.Fake<IConsoleWrapper>();

        _findMealCommand = new FindMealCommand();
    }

    // TODO displays meal ingredients when meal found (when one word passed to -name arg)
    [Test]
    public void ExecuteDisplaysMealIngredientsWhenMealExists()
    {
        // Arrange
        var arguments = new List<Argument>
        {
            new()
            {
                ArgumentName = "name",
                ArgumentValues = new List<string> { "Omelette" }
            }
        };

        var expected = new StringBuilder();
        expected.AppendLine("| Name   | Quantity | Unit |");
        expected.AppendLine("| ------ | -------- | ---- |");
        expected.AppendLine("| Eggs   | 3        | Of   |");
        expected.AppendLine("| Cheese | 50       | G    |");

        // Act
        _findMealCommand.Execute(arguments);

        // Assert
        A.CallTo(() => _consoleWrapper.Write(expected.ToString())).MustHaveHappenedOnceExactly();
    }

    // TODO displays meal ingredients when meal found (when multiple words in quotes passed to -name arg)
    [Test]
    public void ExecuteDisplaysMealIngredientsWhenMealNameContainsMultipleWords()
    {
        // Arrange
        var arguments = new List<Argument>
        {
            new()
            {
                ArgumentName = "name",
                ArgumentValues = new List<string> { "Mushroom Skewer" }
            }
        };

        // Act
        var expected = new StringBuilder();
        expected.AppendLine("| Name      | Quantity | Unit |");
        expected.AppendLine("| --------- | -------- | ---- |");
        expected.AppendLine("| Mushrooms | 50       | G    |");

        // Assert
        A.CallTo(() => _consoleWrapper.Write(expected.ToString())).MustHaveHappenedOnceExactly();
    }

    // TODO displays message saying meal not found when meal not in database
    [Test]
    public void ExecuteDisplaysMessageMealNotFoundWhenMealNotInDatabase()
    {
        // Arrange
        var arguments = new List<Argument>
        {
            new()
            {
                ArgumentName = "name",
                ArgumentValues = new List<string> { "Unfound Meal" }
            }
        };

        var expected = "Meal of name `Unfound Meal` not found, please check your input";

        // Act
        _findMealCommand.Execute(arguments);

        // Assert
        A.CallTo(() => _consoleWrapper.Warn(expected)).MustHaveHappenedOnceExactly();
    }

    private List<MealModel> GetMealModels() => new()
    {
        new()
        {
            Id = 1,
            Name = "Mushroom Skewer",
            Ingredients = new()
            {
                new() { Name = "Mushrooms", Quantity = 50, Unit = Unit.G }
            }
        },
        new()
        {
            Id = 2,
            Name = "Omelette",
            Ingredients = new()
            {
                new() { Name = "Eggs", Quantity = 3, Unit = Unit.Of },
                new() { Name = "Cheese", Quantity = 50, Unit = Unit.G }
            }
        },
        new()
        {
            Id = 3,
            Name = "Sunday Roast",
            Ingredients = new()
            {
                new() { Name = "Chicken", Quantity = 1, Unit = Unit.Of },
                new() { Name = "Various veg", Quantity = 100, Unit = Unit.G }
            }
        }
    };
}

using FightWasteConsole.CommandArguments;
using FightWasteConsole.Commands;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.Models;
using FightWasteConsole.Output;
using FightWasteConsole.Repositories;
using NUnit.Framework.Internal;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Linq;

namespace FightWasteConsoleTests.Commands;

[ExcludeFromCodeCoverage]
[TestFixture]
public class FindMealCommandTests
{
    private IMealRepository _mealRepository;
    private IConsoleWrapper _consoleWrapper;
    private IModelCollectionOutputter<IngredientQuantityModel> _outputter;
    private FindMealCommand _findMealCommand;

    [SetUp]
    public void SetUp()
    {
        _mealRepository = A.Fake<IMealRepository>();
        A.CallTo(() => _mealRepository.GetAll()).Returns(GetMealModels());
        _consoleWrapper = A.Fake<IConsoleWrapper>();
        _outputter = A.Fake<IModelCollectionOutputter<IngredientQuantityModel>>();

        _findMealCommand = new FindMealCommand(_mealRepository, _consoleWrapper, _outputter);
    }

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

        var expectedOutput = new StringBuilder();
        expectedOutput.AppendLine("| Name   | Quantity | Unit |");
        expectedOutput.AppendLine("| ------ | -------- | ---- |");
        expectedOutput.AppendLine("| Eggs   | 3        | Of   |");
        expectedOutput.AppendLine("| Cheese | 50       | G    |");

        A.CallTo(() => _outputter.GetListAsCollection(A<List<IngredientQuantityModel>>.Ignored)).Returns(expectedOutput.ToString());

        // Act
        _findMealCommand.Execute(arguments);

        // Assert
        A.CallTo(() => _outputter.GetListAsCollection(A<List<IngredientQuantityModel>>.That.Matches(list =>
            list[0].Name == "Eggs" && list[0].Quantity == 3 && list[0].Unit == Unit.Of
            && list[1].Name == "Cheese" && list[1].Quantity == 50 && list[1].Unit == Unit.G
            && list.Count == 2
        )));

        A.CallTo(() => _consoleWrapper.Write(expectedOutput.ToString())).MustHaveHappenedOnceExactly();
    }

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

        var expectedOutput = new StringBuilder();
        expectedOutput.AppendLine("| Name      | Quantity | Unit |");
        expectedOutput.AppendLine("| --------- | -------- | ---- |");
        expectedOutput.AppendLine("| Mushrooms | 50       | G    |");

        A.CallTo(() => _outputter.GetListAsCollection(A<List<IngredientQuantityModel>>.Ignored)).Returns(expectedOutput.ToString());

        // Act
        _findMealCommand.Execute(arguments);

        // Assert
        A.CallTo(() => _outputter.GetListAsCollection(A<List<IngredientQuantityModel>>.That.Matches(list =>
            list[0].Name == "Mushroom" && list[0].Quantity == 50 && list[0].Unit == Unit.G
            && list.Count == 1
        )));

        A.CallTo(() => _consoleWrapper.Write(expectedOutput.ToString())).MustHaveHappenedOnceExactly();
    }

    [Ignore("// TODO Need to work out why can't make a null MealModel in the tests - tested manually")]
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

        A.CallTo(_mealRepository).WithReturnType<string>().Returns(null);

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

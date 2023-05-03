using FightWasteConsole.Commands;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.Models;
using FightWasteConsole.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace FightWasteConsoleTests.Commands;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ShowAllMealsCommandTests
{
    private IMealRepository _mealRepository;
    private IConsoleWrapper _consoleWrapper;
    private ShowAllMealsCommand _command;

    [SetUp]
    public void SetUp()
    {
        _mealRepository = A.Fake<IMealRepository>();
        _consoleWrapper = A.Fake<IConsoleWrapper>();

        A.CallTo(() => _mealRepository.GetAll()).Returns(GetMealModels());

        _command = new ShowAllMealsCommand(_mealRepository, _consoleWrapper);
    }

    [Test]
    public void ExecuteCallsMealRepoAndOutputsAllMealsToTheConsole()
    {
        // Act
        _command.Execute();

        // Assert
        A.CallTo(() => _mealRepository.GetAll()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _consoleWrapper.Write("Test Meal\r\nAverage Omelette"))
            .MustHaveHappenedOnceExactly();
    }

    private IEnumerable<MealModel> GetMealModels() => new List<MealModel>()
    {
        new()
        {
            Id = 1,
            Name = "Test Meal",
            Ingredients = new()
            {
                new() { Name = "Mushrooms", Quantity = 50, Unit = Unit.G },
                new() { Name = "Watermelon", Quantity = 1, Unit = Unit.Of }
            }
        },
        new()
        {
            Id = 2,
            Name = "Average Omelette",
            Ingredients = new()
            {
                new() { Name = "Egg", Quantity = 3, Unit = Unit.Of },
                new() { Name = "Ham", Quantity = 30, Unit = Unit.G },
                new() { Name = "Cheese", Quantity = 30, Unit = Unit.G }
            }
        }
    };

}

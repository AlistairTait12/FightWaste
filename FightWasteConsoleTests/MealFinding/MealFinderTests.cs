using FightWasteConsole.MealFinding;
using FightWasteConsole.Models;
using FightWasteConsole.Repositories;

namespace FightWasteConsoleTests.MealFinding;

[TestFixture]
public class MealFinderTests
{
    private IRepository<MealModel> _repository;
    private IConsoleWrapper _consoleWrapper;
    private MealFinder _mealFinder;

    [SetUp]
    public void SetUp()
    {
        _repository = A.Fake<IRepository<MealModel>>();
        A.CallTo(() => _repository.GetAll()).Returns(GetFakeMeals());

        _consoleWrapper = A.Fake<IConsoleWrapper>();

        _mealFinder = new MealFinder(_repository, _consoleWrapper);
    }

    [Test]
    public void FindMealByNameReturnsMealWhenItExistsInRepositoryAndConfirmsToUser()
    {
        // Arrange
        var expected = new MealModel { Name = "Soup" };

        // Act
        A.CallTo(() => _consoleWrapper.Read()).Returns("Soup");
        var actual = _mealFinder.FindMealByName();

        // Assert
        actual.Should().BeEquivalentTo(expected);
        A.CallTo(() => _consoleWrapper.Confirm("Meal 'Soup' added")).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void FindMealByNameContinuesToAskUserToEnterMealWhenNotFoundAndDisplaysMealNotFound()
    {
        // Arrange
        var expected = new MealModel { Name = "Pizza" };

        // Act
        A.CallTo(() => _consoleWrapper.Read()).Returns("Pizza");
        A.CallTo(() => _consoleWrapper.Read()).ReturnsNextFromSequence("Pizz");
        var actual = _mealFinder.FindMealByName();

        // Assert
        actual.Should().BeEquivalentTo(expected);
        A.CallTo(() => _consoleWrapper.Read()).MustHaveHappenedTwiceExactly();
        A.CallTo(() => _consoleWrapper.Warn("Meal 'Pizz' not found, please enter another meal")).MustHaveHappenedOnceExactly();
        A.CallTo(() => _consoleWrapper.Confirm("Meal 'Pizza' added")).MustHaveHappenedOnceExactly();
    }

    private IEnumerable<MealModel> GetFakeMeals() => new List<MealModel>
    {
        new() { Name = "Soup" },
        new() { Name = "Pizza" },
        new() { Name = "Vol-au-Vent" },
        new() { Name = "Sushi" }
    };
}

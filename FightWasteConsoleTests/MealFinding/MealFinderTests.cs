using FakeItEasy;
using FightWasteConsole.MealFinding;
using FightWasteConsole.Models;
using FightWasteConsole.Repositories;

namespace FightWasteConsoleTests.MealFinding;

[TestFixture]
public class MealFinderTests
{
    private IRepository<MealModel> _repository;
    private MealFinder _mealFinder;

    [SetUp]
    public void SetUp()
    {
        _repository = A.Fake<IRepository<MealModel>>();
        A.CallTo(() => _repository.GetAll()).Returns(GetFakeMeals());

        _mealFinder = new MealFinder(_repository);
    }

    [Test]
    public void FindMealByNameReturnsMealWhenItExistsInRepository()
    {
        // Arrange
        var expected = new MealModel { Name = "Soup" };

        // Act
        var actual = _mealFinder.FindMealByName("Soup");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    private IEnumerable<MealModel> GetFakeMeals() => new List<MealModel>
    {
        new() { Name = "Soup" },
        new() { Name = "Pizza" },
        new() { Name = "Vol-au-Vent" },
        new() { Name = "Sushi" }
    };
}

using FightWasteConsole.DataAccess;
using FightWasteConsole.Models;
using FightWasteConsole.Repositories;

namespace FightWasteConsoleTests.Repositories;

[TestFixture]
public class MealRepositoryTests
{
    private MealRepository _mealRepository;
    private IDataAccess<MealModel> _dataAccess;

    [SetUp]
    public void SetUp()
    {
        _dataAccess = A.Fake<IDataAccess<MealModel>>();
        A.CallTo(() => _dataAccess.GetData()).Returns(GetFakeMealData());

        _mealRepository = new MealRepository(_dataAccess);
    }

    [Test]
    public void GetMealByNameReturnsCorrectModelWhenPresentInDataBase()
    {
        // Arrange
        var expected = new MealModel
        {
            Id = 2,
            Name = "Average Omelette",
            Ingredients = new()
            {
                new() { Name = "Egg", Quantity = 3, Unit = Unit.Of },
                new() { Name = "Ham", Quantity = 30, Unit = Unit.G },
                new() { Name = "Cheese", Quantity = 30, Unit = Unit.G }
            }
        };

        // Act
        var actual = _mealRepository.GetMealByName("Average Omelette");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetMealByNameReturnsNullWhenMealNotInDataBase()
    {
        // Act
        var actual = _mealRepository.GetMealByName("Noneggsistent Meal");

        // Assert
        actual.Should().BeNull();
    }

    [TestCase("Average omelette")]
    [TestCase("aVeRaGe OmElEtTe")]
    [TestCase("AVERAGE omelette")]
    public void GetMealByNameFindsMealWhenGivenStringsOfVariedCasing(string mealToFind)
    {
        // Arrange
        var expected = new MealModel
        {
            Id = 2,
            Name = "Average Omelette",
            Ingredients = new()
            {
                new() { Name = "Egg", Quantity = 3, Unit = Unit.Of },
                new() { Name = "Ham", Quantity = 30, Unit = Unit.G },
                new() { Name = "Cheese", Quantity = 30, Unit = Unit.G }
            }
        };

        // Act
        var actual = _mealRepository.GetMealByName(mealToFind);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [TestCase("Average Omelette.")]
    [TestCase("AverageOmelette")]
    [TestCase("averageomelette")]
    public void GetMealByNameReturnsNullWhenGivenStringsOfVariedPunctuation(string mealToFind)
    {
        // Act
        var actual = _mealRepository.GetMealByName(mealToFind);

        // Assert
        actual.Should().BeNull();
    }

    private IEnumerable<MealModel> GetFakeMealData() => new List<MealModel>
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

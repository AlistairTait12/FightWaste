using FightWasteConsole.DataAccess;
using FightWasteConsole.Models;

namespace FightWasteConsoleTests.DataAccess;

[TestFixture]
public class JsonDataAccessTests
{
    private JsonDataAccess<MealModel> _jsonDataAccess;

    [SetUp]
    public void SetUp()
    {
        _jsonDataAccess = new
            ($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}..\\..\\..\\DataAccess\\TestMealModels.json");
    }

    [Test]
    public void GetDataReturnsModelFromFile()
    {
        // Act
        var actual = _jsonDataAccess.GetData();

        // Assert
        actual.Should().BeOfType<List<MealModel>>();
    }

    [Test]
    public void GetDataReturnsDataAsModel()
    {
        // Arrange
        var expected = GetExpectedMealModels();

        // Act
        var actual = _jsonDataAccess.GetData();

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    private List<MealModel> GetExpectedMealModels() => new()
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
        }
    };
}

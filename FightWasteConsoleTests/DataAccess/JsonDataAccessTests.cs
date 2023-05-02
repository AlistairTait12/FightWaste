using FightWasteConsole.DataAccess;
using FightWasteConsole.Models;
using FightWasteConsole.Options;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace FightWasteConsoleTests.DataAccess;

[ExcludeFromCodeCoverage]
[TestFixture]
public class JsonDataAccessTests
{
    private JsonDataAccess<MealModel> _jsonDataAccess;

    [SetUp]
    public void SetUp()
    {
        var fakeOptions = new FightWasteOptions()
        {
            MealFilePath = $@"{AppDomain.CurrentDomain.BaseDirectory}..\..\..\DataAccess\TestMealModels.json",
        };

        _jsonDataAccess = new (Options.Create(fakeOptions));
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

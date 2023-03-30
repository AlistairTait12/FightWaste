using FightWasteConsole.Aggregation;
using FightWasteConsole.Models;

namespace FightWasteConsoleTests.Aggregation;

[TestFixture]
public class IngredientAggregatorTests
{
    private IngredientAggregator _aggregator;

    [SetUp]
    public void SetUp()
    {
        _aggregator = new IngredientAggregator();
    }

    [Test]
    public void CombineIngredientsReturnsCollectionOfAllIngredientsGroupedByNameShowingOverallQuantity()
    {
        // Arrange
        var expected = new List<IngredientQuantityModel>
        {
            new() { Name = "Eggs", Quantity = 5, Unit = Unit.Of },
            new() { Name = "Cheese", Quantity = 100, Unit = Unit.G },
            new() { Name = "Peppers", Quantity = 150, Unit = Unit.G },
            new() { Name = "Milk", Quantity = 300, Unit = Unit.Ml }
        };

        // Act
        var actual = _aggregator.CombineIngredients(GetSplitIngredients());

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    private List<IngredientQuantityModel> GetSplitIngredients() => new()
    {
        new() { Name = "Eggs", Quantity = 2, Unit = Unit.Of },
        new() { Name = "Peppers", Quantity = 50, Unit = Unit.G },
        new() { Name = "Milk", Quantity = 170, Unit = Unit.Ml },
        new() { Name = "Cheese", Quantity = 100, Unit = Unit.G },
        new() { Name = "Eggs", Quantity = 3, Unit = Unit.Of },
        new() { Name = "Peppers", Quantity = 50, Unit = Unit.G },
        new() { Name = "Milk", Quantity = 130, Unit = Unit.Ml },
        new() { Name = "Peppers", Quantity = 50, Unit = Unit.G }
    };
}

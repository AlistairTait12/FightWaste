using FightWasteConsole.Models;
using FightWasteConsole.Output;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FightWasteConsoleTests.Output
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ModelTableOutputterTests
    {
        [Test]
        public void GetGetListAsCollectionReturnsStringOfCollectionAsTable()
        {
            // Arrange
            var models = new List<IngredientQuantityModel>
            {
                new() { Name = "Apples", Quantity = 1, Unit = Unit.Of },
                new() { Name = "Water", Quantity = 100, Unit = Unit.Ml }
            };

            var expected = new StringBuilder();
            expected.AppendLine("| Name   | Quantity | Unit |");
            expected.AppendLine("|--------|----------|------|");
            expected.AppendLine("| Apples | 1        | Of   |");
            expected.AppendLine("| Water  | 100      | Ml   |");

            var outputter = new ModelTableOutputter<IngredientQuantityModel>();

            // Act
            var actual = outputter.GetListAsCollection(models);

            // Assert
            actual.Should().Be(expected.ToString());
        }
    }
}
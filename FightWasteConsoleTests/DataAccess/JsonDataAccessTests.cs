using FightWasteConsole.DataAccess;
using FightWasteConsole.Models;
using System.Configuration;

namespace FightWasteConsoleTests.DataAccess;

[TestFixture]
public class JsonDataAccessTests
{
    [Test]
    public void GetDataReturnsRealData()
    {
        // Arrange
        var jsonDataAccess = new JsonDataAccess<MealModel>
            ($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}..\\..\\..\\DataAccess\\TestMealModels.json");

        // Act
        var data = jsonDataAccess.GetData();

        // Assert
        data.Should().BeOfType<List<MealModel>>();
    }
}

using FightWasteConsole.CommandArguments;
using FightWasteConsole.Commands;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.Models;
using FightWasteConsole.Repositories;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FightWasteConsoleTests.Commands;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ShowMealsCommandTests
{
    private IMealRepository _mealRepository;
    private IConsoleWrapper _consoleWrapper;
    private ShowMealsCommand _showMealsCommand;

    [SetUp]
    public void SetUp()
    {
        _mealRepository = A.Fake<IMealRepository>();
        A.CallTo(() => _mealRepository.GetAll()).Returns(GetMeals());
        _consoleWrapper = A.Fake<IConsoleWrapper>();

        _showMealsCommand = new ShowMealsCommand(_mealRepository, _consoleWrapper);
    }

    [Test]
    public void ExecuteWhenGivenArgumentValueOf3Returns3MealsWhenThereAreAtLeast3InDatabase()
    {
        // Arrange
        var arguments = new List<Argument>
        {
            new()
            {
                ArgumentName = "number",
                ArgumentValues = new() { "3" }
            }
        };

        // Act
        _showMealsCommand.Execute(arguments);

        // Assert
        A.CallTo(() => _consoleWrapper.Write("Sushi\r\nOmelette\r\nRice\r\n")).MustHaveHappenedOnceExactly();
        A.CallTo(() => _mealRepository.GetAll()).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void ExecuteWhenPassedArgumentNameOfNReturnsMealNamesAsNIsAliasForNumberArg()
    {
        // Arrange
        var arguments = new List<Argument>
        {
            new()
            {
                ArgumentName = "n",
                ArgumentValues = new() { "3" }
            }
        };

        // Act
        _showMealsCommand.Execute(arguments);

        // Assert
        A.CallTo(() => _consoleWrapper.Write("Sushi\r\nOmelette\r\nRice\r\n")).MustHaveHappenedOnceExactly();
        A.CallTo(() => _mealRepository.GetAll()).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void ExecuteWhenGivenMoreMealsThanIsInDataBaseWritesOutMaxNumberOfMealsAndWarnsUser()
    {
        // Arrange
        var arguments = new List<Argument>
        {
            new()
            {
                ArgumentName = "number",
                ArgumentValues = new() { "7" }
            }
        };

        var sb = new StringBuilder();
        sb.AppendLine("Sushi");
        sb.AppendLine("Omelette");
        sb.AppendLine("Rice");
        sb.AppendLine("Jelly");
        sb.AppendLine("Meringue");
        var expectedMessage = sb.ToString();

        // Act
        _showMealsCommand.Execute(arguments);

        // Assert
        A.CallTo(() => _consoleWrapper.Warn("Showing maximum (5) meals in database")).MustHaveHappenedOnceExactly();
        A.CallTo(() => _consoleWrapper.Write(expectedMessage)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _mealRepository.GetAll()).MustHaveHappenedOnceExactly();
    }

    private IEnumerable<MealModel> GetMeals()
        => new List<MealModel>
        {
            new MealModel { Name = "Sushi" },
            new MealModel { Name = "Omelette" },
            new MealModel { Name = "Rice" },
            new MealModel { Name = "Jelly" },
            new MealModel { Name = "Meringue" }
        };
}

using FightWasteConsole.Aggregation;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.FileWriter;
using FightWasteConsole.IngredientsListProcessing;
using FightWasteConsole.Models;
using FightWasteConsole.Output;
using FightWasteConsole.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace FightWasteConsoleTests.IngredientsListProcessing;

[ExcludeFromCodeCoverage]
[TestFixture]
public class IngredientsListProcessorTests
{
    private IModelCollectionOutputter<IngredientQuantityModel> _outputter;
    private IIngredientAggregator _aggregator;
    private IConsoleWrapper _consoleWrapper;
    private IFileWriter _writer;
    private IMealRepository _mealRepository;
    private IngredientsListProcessor _processor;

    [SetUp]
    public void SetUp()
    {
        _outputter = A.Fake<IModelCollectionOutputter<IngredientQuantityModel>>();
        _aggregator = A.Fake<IIngredientAggregator>();
        _consoleWrapper = A.Fake<IConsoleWrapper>();
        _writer = A.Fake<IFileWriter>();
        _mealRepository = A.Fake<IMealRepository>();

        A.CallTo(() => _mealRepository.GetAll()).Returns(GetMealModels());

        _processor = new IngredientsListProcessor(_outputter,
            _aggregator, _consoleWrapper, _writer, _mealRepository);
    }

    [Test]
    public void ProduceIngredientsListMakesAllTheRightCalls()
    {
        // Arrange
        A.CallTo(() => _consoleWrapper.Read()).Returns("END");

        // Act
        _processor.ProduceIngredientsList();

        // Assert
        A.CallTo(() => _consoleWrapper.Write("Please enter your meals for the week")).MustHaveHappenedOnceExactly();
        A.CallTo(() => _aggregator.CombineIngredients(A<IEnumerable<IngredientQuantityModel>>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _outputter.GetListAsCollection(A<List<IngredientQuantityModel>>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _writer.WriteIngredientsToFile(A<IEnumerable<IngredientQuantityModel>>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void ProduceIngredientsListWhenUserEntersShowAllDisplaysAllMeals()
    {
        // Arrange
        A.CallTo(() => _consoleWrapper.Read()).Returns("End");
        A.CallTo(() => _consoleWrapper.Read()).ReturnsNextFromSequence("showall");

        // Act
        _processor.ProduceIngredientsList();

        // Assert
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

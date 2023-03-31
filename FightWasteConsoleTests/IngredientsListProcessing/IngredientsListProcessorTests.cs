using FightWasteConsole.Aggregation;
using FightWasteConsole.IngredientsListProcessing;
using FightWasteConsole.MealFinding;
using FightWasteConsole.Models;
using FightWasteConsole.Output;

namespace FightWasteConsoleTests.IngredientsListProcessing;

[TestFixture]
public class IngredientsListProcessorTests
{
    private IModelCollectionOutputter<IngredientQuantityModel> _outputter;
    private IMealFinder _mealFinder;
    private IIngredientAggregator _aggregator;
    private IConsoleWrapper _consoleWrapper;

    [SetUp]
    public void SetUp()
    {
        _outputter = A.Fake<IModelCollectionOutputter<IngredientQuantityModel>>();
        _mealFinder = A.Fake<IMealFinder>();
        _aggregator = A.Fake<IIngredientAggregator>();
        _consoleWrapper = A.Fake<IConsoleWrapper>();
    }

    [Test]
    public void ProduceIngredientsListMakesAllTheRightCalls()
    {
        // Arrange
        var processor = new IngredientsListProcessor(_outputter, _mealFinder, _aggregator, _consoleWrapper);

        // Act
        processor.ProduceIngredientsList();

        // Assert
        A.CallTo(() => _consoleWrapper.Write("Please enter your meals for the week")).MustHaveHappenedOnceExactly();
        A.CallTo(() => _mealFinder.FindMealByName()).MustHaveHappened();
        A.CallTo(() => _aggregator.CombineIngredients(A<IEnumerable<IngredientQuantityModel>>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _outputter.GetListAsCollection(A<List<IngredientQuantityModel>>.Ignored)).MustHaveHappenedOnceExactly();
    }
}

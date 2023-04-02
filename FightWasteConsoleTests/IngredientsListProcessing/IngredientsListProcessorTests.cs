using FightWasteConsole.Aggregation;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.FileWriter;
using FightWasteConsole.IngredientsListProcessing;
using FightWasteConsole.Models;
using FightWasteConsole.Output;

namespace FightWasteConsoleTests.IngredientsListProcessing;

[TestFixture]
public class IngredientsListProcessorTests
{
    private IModelCollectionOutputter<IngredientQuantityModel> _outputter;
    private IIngredientAggregator _aggregator;
    private IConsoleWrapper _consoleWrapper;
    private IFileWriter _writer;

    [SetUp]
    public void SetUp()
    {
        _outputter = A.Fake<IModelCollectionOutputter<IngredientQuantityModel>>();
        _aggregator = A.Fake<IIngredientAggregator>();
        _consoleWrapper = A.Fake<IConsoleWrapper>();
        _writer = A.Fake<IFileWriter>();
    }

    [Test]
    public void ProduceIngredientsListMakesAllTheRightCalls()
    {
        // Arrange
        var processor = new IngredientsListProcessor(_outputter,
            _aggregator, _consoleWrapper, _writer);

        // Act
        processor.ProduceIngredientsList();

        // Assert
        A.CallTo(() => _consoleWrapper.Write("Please enter your meals for the week")).MustHaveHappenedOnceExactly();
        A.CallTo(() => _aggregator.CombineIngredients(A<IEnumerable<IngredientQuantityModel>>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _outputter.GetListAsCollection(A<List<IngredientQuantityModel>>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _writer.WriteIngredientsToFile(A<IEnumerable<IngredientQuantityModel>>.Ignored)).MustHaveHappenedOnceExactly();
    }
}

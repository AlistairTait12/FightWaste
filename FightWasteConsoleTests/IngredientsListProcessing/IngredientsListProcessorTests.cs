using FightWasteConsole.Aggregation;
using FightWasteConsole.ConsoleWrapper;
using FightWasteConsole.FileWriter;
using FightWasteConsole.IngredientsListProcessing;
using FightWasteConsole.Models;
using FightWasteConsole.Output;
using FightWasteConsole.Repositories;

namespace FightWasteConsoleTests.IngredientsListProcessing;

[TestFixture]
public class IngredientsListProcessorTests
{
    private IModelCollectionOutputter<IngredientQuantityModel> _outputter;
    private IIngredientAggregator _aggregator;
    private IConsoleWrapper _consoleWrapper;
    private IFileWriter _writer;
    private IMealRepository _mealRepository;

    [SetUp]
    public void SetUp()
    {
        _outputter = A.Fake<IModelCollectionOutputter<IngredientQuantityModel>>();
        _aggregator = A.Fake<IIngredientAggregator>();
        _consoleWrapper = A.Fake<IConsoleWrapper>();
        _writer = A.Fake<IFileWriter>();
        _mealRepository = A.Fake<IMealRepository>();
    }

    [Test]
    public void ProduceIngredientsListMakesAllTheRightCalls()
    {
        // Arrange
        var processor = new IngredientsListProcessor(_outputter,
            _aggregator, _consoleWrapper, _writer, _mealRepository);

        A.CallTo(() => _consoleWrapper.Read()).Returns("END");

        // Act
        processor.ProduceIngredientsList();

        // Assert
        A.CallTo(() => _consoleWrapper.Write("Please enter your meals for the week")).MustHaveHappenedOnceExactly();
        A.CallTo(() => _aggregator.CombineIngredients(A<IEnumerable<IngredientQuantityModel>>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _outputter.GetListAsCollection(A<List<IngredientQuantityModel>>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _writer.WriteIngredientsToFile(A<IEnumerable<IngredientQuantityModel>>.Ignored)).MustHaveHappenedOnceExactly();
    }
}

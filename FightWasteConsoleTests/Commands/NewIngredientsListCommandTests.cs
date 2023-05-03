using FightWasteConsole.Commands;
using FightWasteConsole.IngredientsListProcessing;
using System.Diagnostics.CodeAnalysis;

namespace FightWasteConsoleTests.Commands;

[ExcludeFromCodeCoverage]
[TestFixture]
public class NewIngredientsListCommandTests
{
    private IIngredientsListProcessor _processor;
    private NewIngredientsListCommand _newIngredientsListCommand;

    [SetUp]
    public void SetUp()
    {
        _processor = A.Fake<IIngredientsListProcessor>();
        _newIngredientsListCommand = new NewIngredientsListCommand();
    }

    // TODO Test that a call to Fake IIngredientsListProcessor.ProduceIngredientsList()
    // was called when correct command is called
    [Test]
    public void ExecuteCallsProduceIngredientsList()
    {
        // Act
        _newIngredientsListCommand.Execute();

        // Assert
        A.CallTo(() => _processor.ProduceIngredientsList()).MustHaveHappenedOnceExactly();
    }
}

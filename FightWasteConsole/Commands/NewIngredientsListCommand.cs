using FightWasteConsole.IngredientsListProcessing;

namespace FightWasteConsole.Commands;

public class NewIngredientsListCommand : ICommand
{
    private readonly IIngredientsListProcessor _processor;

    public NewIngredientsListCommand(IIngredientsListProcessor processor)
    {
        _processor = processor;
    }

    public IEnumerable<string> Aliases => new List<string>
    {
        CommandStringConstants.NEWINGREDIENTSLIST,
        CommandStringConstants.NEWLIST,
        CommandStringConstants.NEW
    };

    public void Execute()
    {
        _processor.ProduceIngredientsList();
    }
}

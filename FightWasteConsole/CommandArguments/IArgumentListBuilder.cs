namespace FightWasteConsole.CommandArguments;

public interface IArgumentListBuilder
{
    List<Argument> Build(string target);
}

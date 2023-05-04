namespace FightWasteConsole.CommandArguments;

public class ArgumentListBuilder : IArgumentListBuilder
{
    public List<Argument> Build(string target)
    {
        var args = new List<Argument>();

        var argStrings = GetArgTargets(target).Skip(1).ToList();

        argStrings.ForEach(arg =>
        {
            args.Add(GetSingleArgument(arg));
        });

        return args;
    }

    private List<string> GetArgTargets(string fullString) =>
        fullString.Split('-').ToList();

    private Argument GetSingleArgument(string argString)
    {
        var stringList = argString.TrimEnd().Split(' ').ToList();
        var values = stringList.Skip(1).ToList();

        return new Argument
        {
            ArgumentName = stringList.First(),
            ArgumentValues = values
        };
    }
}

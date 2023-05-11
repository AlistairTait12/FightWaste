using System.Text.RegularExpressions;

namespace FightWasteConsole.CommandArguments;

public class ArgumentListBuilder : IArgumentListBuilder
{
    public List<Argument> Build(string target)
    {
        ValidateTarget(target);

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
        var argName = argString.Split(' ').FirstOrDefault();

        var valueString = string.Join(' ', argString.Split(' ').Skip(1));

        var values = GetArgumentValues(valueString);

        return new Argument
        {
            ArgumentName = argName,
            ArgumentValues = values
        };
    }

    private List<string> GetArgumentValues(string valueString)
    {
        var words = valueString.Split(new char[] { ' ' });

        var finalList = new List<string>();
        var sublist = new List<string>();

        var addToSubList = false;
        for (var i = 0; i < words.Length; i++)
        {
            if (words[i].StartsWith("'") && words[i].EndsWith("'"))
            {
                finalList.Add(words[i].Replace("'", string.Empty));
            }
            else if (words[i].StartsWith("'"))
            {
                sublist.Add(words[i]);
                addToSubList = true;
            }
            else if (words[i].EndsWith("'"))
            {
                sublist.Add(words[i]);
                finalList.Add(string.Join(' ', sublist).Replace("'", string.Empty));
                addToSubList = false;
            }
            else if (addToSubList)
            {
                sublist.Add(words[i]);
            }
            else
            {
                finalList.Add(words[i]);
            }
        }

        finalList.RemoveAll(x => string.IsNullOrEmpty(x));
        return finalList;
    }

    private void ValidateTarget(string target)
    {
        var invalidPattern = new Regex(@"([-])\1+");

        if (invalidPattern.IsMatch(target))
        {
            throw new InvalidOperationException();
        }
    }
}

namespace FightWasteConsole.MealFinding;

public class ConsoleWrapper : IConsoleWrapper
{
    public string Read()
    {
        var message = Console.ReadLine();
        return message;
    }

    public void Write(string message)
    {
        Console.WriteLine(message);
    }
}

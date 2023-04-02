namespace FightWasteConsole.ConsoleWrapper;

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

    public void Confirm(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void Warn(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

namespace FightWasteConsole.MealFinding;

public interface IConsoleWrapper
{
    string Read();
    void Write(string message);
    void Confirm(string message);
    void Warn(string message);
}

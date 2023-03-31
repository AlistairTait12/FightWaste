using FightWasteConsole.Models;

namespace FightWasteConsole.FileWriter;

public interface IFileWriter
{
    void WriteIngredientsToFile(IEnumerable<IngredientQuantityModel> ingredients);
}

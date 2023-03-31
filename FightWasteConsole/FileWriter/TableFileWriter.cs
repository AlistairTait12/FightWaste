using FightWasteConsole.Models;
using FightWasteConsole.Output;

namespace FightWasteConsole.FileWriter;

public class TableFileWriter : IFileWriter
{
    private readonly string _filePath;
    private readonly IModelCollectionOutputter<IngredientQuantityModel> _modelCollectionOutputter;

    public TableFileWriter(string filePath,
        IModelCollectionOutputter<IngredientQuantityModel> modelCollectionOutputter)
    {
        _filePath = filePath;
        _modelCollectionOutputter = modelCollectionOutputter;
    }

    public void WriteIngredientsToFile(IEnumerable<IngredientQuantityModel> ingredients)
    {
        var content = _modelCollectionOutputter.GetListAsCollection(ingredients.ToList());

        var uniqueName = DateTime.Now.ToString("yyyy MM dd HH mm ss");
        var fileName = $"{_filePath}Ingredients_{uniqueName}.txt";

        if (!Directory.Exists(_filePath))
        {
            Directory.CreateDirectory(_filePath);
        }

        File.Create(fileName).Dispose();
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.Write(content);
            writer.Close();
        }
    }
}

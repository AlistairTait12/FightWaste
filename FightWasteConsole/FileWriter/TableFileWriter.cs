using FightWasteConsole.Models;
using FightWasteConsole.Options;
using FightWasteConsole.Output;
using Microsoft.Extensions.Options;

namespace FightWasteConsole.FileWriter;

public class TableFileWriter : IFileWriter
{
    private readonly string _outputPath;
    private readonly IModelCollectionOutputter<IngredientQuantityModel> _modelCollectionOutputter;

    public TableFileWriter(IOptions<FightWasteOptions> options,
        IModelCollectionOutputter<IngredientQuantityModel> modelCollectionOutputter)
    {
        _outputPath = options.Value.ListOutputFolderPath;
        _modelCollectionOutputter = modelCollectionOutputter;
    }

    public void WriteIngredientsToFile(IEnumerable<IngredientQuantityModel> ingredients)
    {
        var content = _modelCollectionOutputter.GetListAsCollection(ingredients.ToList());

        var uniqueName = DateTime.Now.ToString("yyyy MM dd HH mm ss");
        var fileName = $"{_outputPath}Ingredients_{uniqueName}.txt";

        if (!Directory.Exists(_outputPath))
        {
            Directory.CreateDirectory(_outputPath);
        }

        File.Create(fileName).Dispose();
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.Write(content);
            writer.Close();
        }
    }
}

using FightWasteConsole.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FightWasteConsole.DataAccess;

public class JsonDataAccess<IModel> : IDataAccess<IModel>
{
    private readonly string _filePath;

    // HACK: This should eventually be replaced by real database access
    public JsonDataAccess(IOptions<FightWasteOptions> options)
    {
        _filePath = options.Value.MealFilePath;
    }

    public IEnumerable<IModel> GetData()
    {
        // TODO: Getting Unit enum value from a string in the JSON, instead of
        //  a numerical representation 
        var fileStream = File.ReadAllText(_filePath);
        var items = JsonSerializer.Deserialize<List<IModel>>(fileStream);
        return items.AsEnumerable();
    }
}

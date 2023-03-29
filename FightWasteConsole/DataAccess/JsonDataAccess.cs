using FightWasteConsole.Models;
using System.Text.Json;

namespace FightWasteConsole.DataAccess;

public class JsonDataAccess<IModel> : IDataAccess<IModel>
{
    private readonly string _filePath;

    // HACK: This should eventually be replaceed by real database access
    public JsonDataAccess(string filePath)
    {
        _filePath = filePath;
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

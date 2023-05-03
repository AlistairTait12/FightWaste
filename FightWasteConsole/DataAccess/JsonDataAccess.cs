using FightWasteConsole.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        var jsonOptions = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        };

        var fileStream = File.ReadAllText(_filePath);
        var items = JsonSerializer.Deserialize<List<IModel>>(fileStream, jsonOptions);
        return items.AsEnumerable();
    }
}

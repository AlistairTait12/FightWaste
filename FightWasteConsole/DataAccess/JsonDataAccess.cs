using FightWasteConsole.Models;
using System.Text.Json;

namespace FightWasteConsole.DataAccess;

public class JsonDataAccess<IModel> : IDataAccess<IModel>
{
    private readonly string _filePath;

    public JsonDataAccess(string filePath)
    {
        _filePath = filePath;
    }

    public IEnumerable<IModel> GetData()
    {
        throw new NotImplementedException();
    }
}

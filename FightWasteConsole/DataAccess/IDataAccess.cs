namespace FightWasteConsole.DataAccess;

public interface IDataAccess<IModel>
{
    IEnumerable<IModel> GetData();
}

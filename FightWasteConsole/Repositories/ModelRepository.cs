using FightWasteConsole.DataAccess;

namespace FightWasteConsole.Repositories;

public class ModelRepository<IModel> : IRepository<IModel>
{
    protected readonly IDataAccess<IModel> _dataAccess;

    public ModelRepository(IDataAccess<IModel> dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public IEnumerable<IModel> GetAll()
    {
        return _dataAccess.GetData();
    }
}

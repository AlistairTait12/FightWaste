using FightWasteConsole.DataAccess;

namespace FightWasteConsole.Repositories;

public class ModelRepository<IModel> : IRepository<IModel>
{
    private readonly IDataAccess<IModel> _dataAccess;

    public ModelRepository(IDataAccess<IModel> dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public IEnumerable<IModel> GetAll()
    {
        throw new NotImplementedException();
    }
}

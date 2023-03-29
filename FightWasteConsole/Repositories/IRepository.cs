namespace FightWasteConsole.Repositories;

public interface IRepository<IModel>
{
    IEnumerable<IModel> GetAll();
}

namespace FightWasteConsole.Output
{
    public interface IModelCollectionOutputter<T> where T : class
    {
        string GetListAsCollection(List<T> models);
    }
}

namespace FightWasteConsole.Output;

public interface IModelCollectionOutputter<T> where T : class
{
    // TODO Rename the method to GetListAsStringOutput()
    string GetListAsCollection(List<T> models);
}


namespace GraphLoom.Mapper.Core.InputSource
{
    public interface IEntityResult
    {
        bool HasNext();
        IEntity NextEntity();
    }
}

namespace GraphLoom.Mapper
{
    public interface IGenericGraph
    {
        bool IsEmpty { get; }
        void Merge(IGenericGraph graph);
    }
}

using VDS.RDF;

namespace GraphLoom.Mapper.RDF
{
    public class VDSGraph : Graph, IGenericGraph
    {
        public void Merge(IGenericGraph genericGraph)
        {
            base.Merge((Graph)genericGraph);
        }
    }
}

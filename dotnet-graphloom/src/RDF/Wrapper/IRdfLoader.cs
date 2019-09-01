using VDS.RDF;

namespace GraphLoom.Mappers.Rdf.Wrapper
{
    public interface IRdfLoader
    {
        IGraph LoadFromFile(IGraph graph, string filename, IRdfReader parser);
    }
}

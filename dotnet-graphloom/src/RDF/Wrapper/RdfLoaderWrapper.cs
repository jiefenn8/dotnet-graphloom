using System;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace GraphLoom.Mapper.RDF.Wrapper
{
    public class RdfLoaderWrapper : IRdfLoader
    {
        public IGraph LoadFromFile(IGraph graph, string filename, IRdfReader parser)
        {
            FileLoader.Load(graph, filename, parser);
            return graph;
        }
    }
}

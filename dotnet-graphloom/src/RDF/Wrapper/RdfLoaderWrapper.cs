using System;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace GraphLoom.Mapper.RDF.Wrapper
{
    public class RdfLoaderWrapper : IRdfLoader
    {
        public void LoadFromFile(IGraph graph, string filename, IRdfReader parser)
        {
            FileLoader.Load(graph, filename, parser);
        }

        public void LoadFromUri(IGraph graph, Uri uri, IRdfReader parser)
        {
            UriLoader.Load(graph, uri, parser);
        }

    }
}

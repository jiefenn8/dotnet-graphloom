using System;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF.Wrapper
{
    public interface IRdfLoader
    {
        void LoadFromFile(IGraph graph, string filename, IRdfReader parser);
        void LoadFromUri(IGraph graph, Uri uri, IRdfReader parser);
    }
}

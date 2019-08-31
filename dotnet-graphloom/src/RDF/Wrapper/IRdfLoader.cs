using System;
using System.Collections.Generic;
using System.Text;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF.Wrapper
{
    public interface IRdfLoader
    {
        IGraph LoadFromFile(IGraph graph, string filename, IRdfReader parser);
    }
}

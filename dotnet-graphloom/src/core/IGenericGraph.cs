using System;
using System.Collections.Generic;
using System.Text;
using VDS.RDF;

namespace GraphLoom.Mapper
{
    public interface IGenericGraph
    {
        bool IsEmpty { get; }
        void Merge(IGenericGraph graph);
    }
}

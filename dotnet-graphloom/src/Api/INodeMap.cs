using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.Mapper.Api
{
    //
    // Summary:
    //     This interface defines the base method that manages the mapping of
    //      any nodes to their graph node term.
    public interface INodeMap
    {
        //
        // Summary:
        //     Returns a generated node term using the node value found from a 
        //     specified column_name in NodeMap using the provided entity records.
        //      
        // Parmeters:
        //   row:
        //     The row to find the column data.
        //
        // Returns:
        //     The term node generated from column value.
        INode GenerateNodeTerm(IReadOnlyDictionary<string, string> row);
    }
}

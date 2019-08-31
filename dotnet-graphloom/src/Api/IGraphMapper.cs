using VDS.RDF;

namespace GraphLoom.Mapper.Api
{
    //
    // Summary:
    //     This interface defines the base methods that manages the mapping
    //     of an InputSource to IGraph using maps from ConfigMaps.
    public interface IGraphMapper
    {
        //
        // Summary:
        //     Main mapping function converting input source data to graph using ConfigMaps.
        //
        // Parameters:
        //   source:
        //     The source containing the data to map over to graph.
        //   configs:
        //     The configs to manage the retrieval and mapping of data from source.
        //
        // Returns: 
        //     The graph containing the mapped source as a graph model.
        IGraph MapToGraph(IInputSource source, IConfigMaps configs);
    }
}

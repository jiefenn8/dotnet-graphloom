using System;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.Mapper.Api
{
    //
    // Summary:
    //     This interface defines the base methods to express the rules
    //     from many configurations that will map a data-source to a 
    //     graph model.
    public interface IConfigMaps
    {
        //
        // Summary:
        //     Returns the base uri of the gaph given by the mapping document.
        //     
        // Returns:
        //     The map containing base uri.
        Uri BaseUri { get; }

        //
        // Summary:
        //     Returns namespace map of all prefix and uri that was used in the 
        //     mapping document.
        //     
        // Returns:
        //     The map containing all prefixes and their uri.
        INamespaceMapper NamespaceMapper { get; }

        //
        // Summary:
        //     Returns all EntityMap that exists in the mapping configuration.
        //
        // Returns:
        //     All EntityMap associated.
        IReadOnlyCollection<IEntityMap> ListEntityMaps();
    }
}

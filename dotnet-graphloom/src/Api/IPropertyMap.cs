using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.Mappers.Api
{
    //
    // Summary:
    //     This interface defines the base methods that manages the mapping
    //     of entity properties to their graph entity terms.
    public interface IPropertyMap
    {
        //
        // Summary:
        //     Returns a generated entity term object using the value found with 
        //     the id column_name specified in template using record given.
        //
        // Parmeters:
        //   row:
        //     The entity record to find the entity id value.
        //
        // Returns:
        //     The term uri generated frow row id.
        IUriNode GenerateEntityTerm(IReadOnlyDictionary<string, string> entityRow);

        //
        // Summary:
        //     Returns an enumerator of all class associated with the entity.
        //
        // Returns:
        //     The readonly collection of all class uri associated.
        IReadOnlyCollection<IUriNode> ListEntityClasses();
    }
}

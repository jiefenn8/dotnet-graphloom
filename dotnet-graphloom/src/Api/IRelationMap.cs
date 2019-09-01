using VDS.RDF;

namespace GraphLoom.Mappers.Api
{
    //
    // Summary:
    //     This interface defines the base methods that manages the mapping
    //     of relation to their graph relation term.
    public interface IRelationMap
    {

        //
        // Summary:
        //     Returns the RelationMap's relation as a term uri.
        //
        // Returns:
        //     The term uri created from relation.
        IUriNode GetRelationTerm();
    }
}

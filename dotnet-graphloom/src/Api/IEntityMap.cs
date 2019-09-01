using System.Collections.Generic;

namespace GraphLoom.Mappers.Api
{
    //
    // Summary:
    //     This interface defines the base methods that manages the mapping
    //     of data to their graph terms sharing the same entity.
    public interface IEntityMap : ISourceMap, IPropertyMap
    {
        //
        // Summary:
        //     Returns all RelationMap that this EntityMap has.
        //
        // Returns:
        //     All RelationMap associated.
        IReadOnlyCollection<IRelationMap> ListRelationMaps();

        //
        // Summary:
        //     Returns the object E that is paired with given object T.
        //
        // Parmeters:
        //   relationMap:
        //     The relationMap to search for paired ObjectMap.
        //
        // Returns:
        //     The NodeMap found with relationMap key.
        INodeMap GetNodeMapWithRelation(IRelationMap relationMap);

        //
        // Summary:
        //     Checks if EntityMap has any RelationMap and NodeMap pair.
        //
        // Returns:
        //     true if there pair, otherwise false.
        bool HasRelationNodeMaps();
    }
}

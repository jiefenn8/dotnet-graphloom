using System.Collections.Generic;

namespace GraphLoom.Mappers.Api
{
    //
    // Summary:
    //     This interface defines the base method to retrieve data from 
    //     a data-source.
    public interface IInputSource
    {
        //
        // Summary:
        //     Returns a list of records for an entity containing the records
        //     that present the data and its column-type.
        //      
        // Parmeters:
        //   entity:
        //     The entity to get records from data-source.
        //
        // Returns:
        //     The readonly list of records as dictionary.
        IReadOnlyCollection<IReadOnlyDictionary<string, string>> GetEntityRecords(string entity);
    }
}

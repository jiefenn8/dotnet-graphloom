namespace GraphLoom.Mapper.Api
{
    //
    // Summary:
    //     This interface defines the base methods that provides the information
    //     needed to locate and retrieve the desired data from the data-source.
    public interface ISourceMap
    {

        //
        // Summary:
        //     Returns the source id where all of the entity records is stored at.
        //
        // Returns: 
        //     The source id of the entity.
        string SourceName { get; }
    }
}

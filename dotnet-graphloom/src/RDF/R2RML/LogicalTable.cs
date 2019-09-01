using GraphLoom.Mappers.Api;

namespace GraphLoom.Mappers.Rdf.R2rml
{
    //
    // Summary:
    //     Implementation of R2RML LogicalTable with SourceMap interface. 
    public class LogicalTable : ISourceMap
    {
        public LogicalTable(string source)
        {
            SourceName = source;
        }

        public string SourceName { get; }
    }
}

using GraphLoom.Mapper.Api;

namespace GraphLoom.Mapper.RDF.R2RML
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

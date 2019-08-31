using GraphLoom.Mapper.Api;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.RDF.R2RML
{
    //
    // Summary:
    //     Implementation of R2RML ObjectMap with NodeMap interface. 
    public class ObjectMap : INodeMap
    {
        private NodeFactory _nodeFactory = new NodeFactory();
        private string _columnName;

        public ObjectMap(string columnName)
        {
            _columnName = columnName;
        }

        public INode GenerateNodeTerm(IReadOnlyDictionary<string, string> row)
        {
            return _nodeFactory.CreateLiteralNode(row[_columnName]);
        }

        public string GetSourceName()
        {
            return _columnName;
        }

        public void SetSourceName(string source)
        {
            _columnName = source;
        }
    }
}

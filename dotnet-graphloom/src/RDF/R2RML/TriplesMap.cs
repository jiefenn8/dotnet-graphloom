using GraphLoom.Mappers.Api;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VDS.RDF;

namespace GraphLoom.Mappers.Rdf.R2rml
{
    //
    // Summary:
    //     Implementation of R2RML TriplesMap with EntityMap interface. 
    public class TriplesMap : IEntityMap
    {
        private LogicalTable _logicalTable;
        private SubjectMap _subjectMap;
        private Dictionary<IRelationMap, INodeMap> _predicateObjectMaps = new Dictionary<IRelationMap, INodeMap>();

        public TriplesMap(LogicalTable logicalTable, SubjectMap subjectMap)
        {
            _logicalTable = logicalTable;
            _subjectMap = subjectMap;
        }

        public string SourceName => _logicalTable.SourceName;

        public void AddRelationNodePair(IRelationMap predicateMap, INodeMap objectMap)
        {
            _predicateObjectMaps.Add(predicateMap, objectMap);
        }

        public IUriNode GenerateEntityTerm(IReadOnlyDictionary<string, string> entityRow)
        {
            return _subjectMap.GenerateEntityTerm(entityRow);
        }

        public INodeMap GetNodeMapWithRelation(IRelationMap relationMap)
        {
            return _predicateObjectMaps[relationMap];
        }

        public IDictionary<IRelationMap, INodeMap> GetRelationObjectConfigs()
        {
            return _predicateObjectMaps;
        }

        public bool HasRelationNodeMaps()
        {
            return _predicateObjectMaps.Any();
        }

        public IReadOnlyCollection<IUriNode> ListEntityClasses()
        {
            return _subjectMap.ListEntityClasses();
        }

        public IReadOnlyCollection<IRelationMap> ListRelationMaps()
        {
            return new ReadOnlyCollection<IRelationMap>(_predicateObjectMaps.Keys.ToList());
        }
    }
}

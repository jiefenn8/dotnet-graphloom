using GraphLoom.Mappers.Api;
using GraphLoom.Mappers.Exceptions;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.Mappers.Rdf
{
    //
    // Summary:
    //     Implementation of R2RML with GraphMapper interface. 
    //     This class implements the mapping functions to map data-source
    //     to a RDF graph model.
    public class RdfMapper : IGraphMapper
    {
        // Summary:
        //     Main mapping function converting a data-source to a RDF graph
        //     model from the provided DAO and mapping configurations.
        //
        // Parameters:
        //   source:
        //     The source DAO providing access to the targeted entity to map.
        //   configs:
        //     The configs to control the mapping function process.
        //
        // Returns:
        //     The graph of the result from mapping.
        public IGraph MapToGraph(IInputSource source, IConfigMaps configs)
        {
            if(source == null) { throw new MapperException("Cannot retrieve source data from null input source."); }
            if(configs == null) { throw new MapperException("Cannot map source from null config maps.");  }

            IGraph outputGraph = new Graph();
            outputGraph.NamespaceMap.Import(configs.NamespaceMapper);
            outputGraph.Merge(MapSource(source, configs.ListEntityMaps()));
            return outputGraph;
        }

        private IGraph MapSource(IInputSource source, IEnumerable<IEntityMap> triplesMaps)
        {
            IGraph sourceGraph = new Graph();

            foreach (IEntityMap t in triplesMaps)
            {
                sourceGraph.Merge(MapEntity(t, source.GetEntityRecords(t.SourceName)));
            }

            return sourceGraph;
        }

        private IGraph MapEntity(IEntityMap triplesMap, IEnumerable<IReadOnlyDictionary<string, string>> rows)
        {
            IGraph entityGraph = new Graph();
            foreach(IReadOnlyDictionary<string, string> r in rows)
            {
                IUriNode subjectUri = triplesMap.GenerateEntityTerm(r);
                foreach (IUriNode c in triplesMap.ListEntityClasses())
                {
                    entityGraph.Assert(
                        subjectUri,
                        entityGraph.CreateUriNode("rdf:type"),
                        c);
                }
                foreach (IRelationMap rm in triplesMap.ListRelationMaps())
                {
                    entityGraph.Assert(
                        subjectUri,
                        rm.GetRelationTerm(),
                        triplesMap.GetNodeMapWithRelation(rm).GenerateNodeTerm(r));
                }
            }
            return entityGraph;
        }
    }
}

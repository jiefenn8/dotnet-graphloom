using GraphLoom.Mapper.Configuration;
using System;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF
{
    public class TriplesAssembler : BaseStatementAssembler<VDSGraph>
    {
        public override IGenericGraph AssembleEntityStatements(IInputSource input, IStatementsConfig entityMap, IDictionary<string, string> nsMap)
        {
            cancelled = false;
            subjectGraph = new VDSGraph();
            subjectGraph.NamespaceMap.Import(NamespaceHelper.ToIGraphNamespaceMap(nsMap));
            foreach (IDictionary<string, string> row in input.GetEntityRecords(entityMap.GetSourceName()))
            {
                if (cancelled) break;

                Uri subjectURI = URIFactory.FromTemplate(entityMap.GetTemplate(), row);
                if (subjectURI == null) break;

                IUriNode subject = subjectGraph.CreateUriNode(subjectURI);
                subjectGraph.Assert(subject, subjectGraph.CreateUriNode("rdf:type"), subjectGraph.CreateUriNode(entityMap.GetClassName()));

                AssemblePredicateObjectsStatements(subject, row, entityMap.GetRelationObjectConfigs());
            }
            return subjectGraph;
        }

        public override void StopTask() => cancelled = true;

        private void AssemblePredicateObjectsStatements(IUriNode subject, IDictionary<string, string> row, IDictionary<IRelationConfig, IObjectsConfig> predicateObjectMap)
        { 
            foreach(KeyValuePair<IRelationConfig, IObjectsConfig> pair in predicateObjectMap){
                IUriNode predicate = subjectGraph.CreateUriNode(pair.Key.GetRelationName());
                INode obj = subjectGraph.CreateLiteralNode(row[pair.Value.GetSourceName()]);
                subjectGraph.Assert(subject, predicate, obj);
            }
        } 
    }
}

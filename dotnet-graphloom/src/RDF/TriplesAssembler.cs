﻿using GraphLoom.Mapper.Configuration;
using System;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF
{
    public class TriplesAssembler : BaseStatementAssembler<VDSGraph>
    {
        public override IGenericGraph AssembleEntityStatements(IInputSource input, IStatementsConfig entityMap, IDictionary<string, string> nsMap)
        {
            Cancelled = false;
            SubjectGraph = new VDSGraph();
            SubjectGraph.NamespaceMap.Import(NamespaceHelper.ToIGraphNamespaceMap(nsMap));
            foreach (IDictionary<string, string> row in input.GetEntityRecords(entityMap.GetSourceName()))
            {
                if (Cancelled) break;

                Uri subjectURI = URIFactory.FromTemplate(entityMap.GetTemplate(), row);
                if (subjectURI == null) break;

                IUriNode subject = SubjectGraph.CreateUriNode(subjectURI);
                SubjectGraph.Assert(subject, SubjectGraph.CreateUriNode("rdf:type"), SubjectGraph.CreateUriNode(entityMap.GetClassName()));

                AssemblePredicateObjectsStatements(subject, row, entityMap.GetRelationObjectConfigs());
            }
            return SubjectGraph;
        }

        public override void StopTask() => Cancelled = true;

        private void AssemblePredicateObjectsStatements(IUriNode subject, IDictionary<string, string> row, IDictionary<IRelationConfig, IObjectsConfig> predicateObjectMap)
        { 
            foreach(KeyValuePair<IRelationConfig, IObjectsConfig> pair in predicateObjectMap){
                IUriNode predicate = SubjectGraph.CreateUriNode(pair.Key.GetRelationName());
                INode obj = SubjectGraph.CreateLiteralNode(row[pair.Value.GetSourceName()]);
                SubjectGraph.Assert(subject, predicate, obj);
            }
        } 
    }
}

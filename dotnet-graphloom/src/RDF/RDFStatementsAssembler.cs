using GraphLoom.Mapper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF
{
    public class RDFStatementsAssembler
    {
        private IGraph SubjectGraph;
        private bool Cancelled = false;
     
        public virtual IGraph AssembleSubjectStatements(IInputSource input, IStatementsConfig entityMap, INamespaceMapper nsMap)
        {
            Cancelled = false;
            SubjectGraph = new Graph();
            SubjectGraph.NamespaceMap.Import(nsMap);
            foreach (IDictionary<string, string> row in input.GetEntityRecords(entityMap.GetSourceName()))
            {
                if (Cancelled) break;

                Uri subjectURI = RDFUriFactory.FromTemplate(entityMap.GetTemplate(), row);
                if (subjectURI == null) break;

                IUriNode subject = SubjectGraph.CreateUriNode(subjectURI);
                SubjectGraph.Assert(subject, SubjectGraph.CreateUriNode("rdf:type"), SubjectGraph.CreateUriNode(entityMap.GetClassName()));

                AssemblePredicateObjectsStatements(subject, row, entityMap.ListRelationConfigs());
            }
            return SubjectGraph;
        }

        private void AssemblePredicateObjectsStatements(IUriNode subject, IDictionary<string, string> row, List<IRelationConfig> relationConfigs)
        {
            relationConfigs.ForEach(relationConfig =>
            {
                IUriNode predicate = SubjectGraph.CreateUriNode(relationConfig.GetRelationName());
                INode obj = SubjectGraph.CreateLiteralNode(row[relationConfig.GetSourceName()]);
                SubjectGraph.Assert(subject, predicate, obj);
            });
        }

        public virtual void StopTask()
        {
            Cancelled = true;
        }
    }
}

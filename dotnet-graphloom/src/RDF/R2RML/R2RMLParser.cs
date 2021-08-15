using GraphLoom.Mapper.RDF.Configuration;
using GraphLoom.Mapper.RDF.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace GraphLoom.Mapper.RDF.R2RML
{
    public class R2RMLParser
    {
        private readonly string _r2rmlPrefix = "rr";
        private IRdfLoader _rdfLoader;
        private TurtleParser _turtleParser;

        public R2RMLParser()
        {
            _rdfLoader = new RdfLoaderWrapper();
            _turtleParser = new TurtleParser();
        }

        public R2RMLParser(IRdfLoader loader, TurtleParser parser)
        {
            _rdfLoader = loader;
            _turtleParser = parser;
        }

        public R2RMLMap Parse(string filename)
        {
            return Parse(filename, null);
        }

        public R2RMLMap Parse(string filename, Uri baseUri)
        {
            IGraph graph = new Graph();
            graph.BaseUri = baseUri;
            _rdfLoader.LoadFromFile(graph, filename, _turtleParser);

            return MapToR2RMLMap(graph);
        }

        public R2RMLMap Parse(Uri baseUri)
        {
            IGraph graph = new Graph();
            _rdfLoader.LoadFromUri(graph, baseUri, _turtleParser);

            return MapToR2RMLMap(graph);
        }

        private R2RMLMap MapToR2RMLMap(IGraph graph)
        {
            if (!graph.NamespaceMap.HasNamespace(_r2rmlPrefix)) return null;

            R2RMLMap r2rmlMap = new R2RMLMap();
            foreach (string prefix in graph.NamespaceMap.Prefixes)
            {
                r2rmlMap.AddNamespace(prefix, graph.NamespaceMap.GetNamespaceUri(prefix).ToString());
            }

            IEnumerable<INode> nodesList = graph.Triples.SubjectNodes.UriNodes();
            if (!nodesList.Any()) return null;
            foreach (IUriNode uri in nodesList)
            {
                r2rmlMap.AddStatementsConfig(MapToTriplesMap(graph, uri));
            }

            return r2rmlMap;
        }

        private TriplesMap MapToTriplesMap(IGraph graph, IUriNode triplesMapURI)
        {
            TriplesMap triplesMap = new TriplesMap();
            IEnumerable<Triple> nodes = graph.GetTriplesWithSubject(triplesMapURI);
            MapToLogicalTable(graph, nodes.WithPredicate(graph.GetUriNode("rr:logicalTable")), triplesMap);
            MapToSubjectMap(graph, nodes.WithPredicate(graph.GetUriNode("rr:subjectMap")), triplesMap);
            MapToPredicateObjectMap(graph, nodes.WithPredicate(graph.GetUriNode("rr:predicateObjectMap")), triplesMap);

            return triplesMap;
        }

        private void MapToLogicalTable(IGraph graph, IEnumerable<Triple> ltList, TriplesMap output)
        {
            if (ltList.Count() != 1) throw new ArgumentOutOfRangeException("R2RML Rule: Must have ONE LogicalTable per TriplesMap.");
            Triple logicalTable = ltList.First();

            IEnumerable<Triple> tableNameList = graph.GetTriplesWithSubjectPredicate(logicalTable.Object, graph.GetUriNode("rr:tableName"));
            if (tableNameList.Count() != 1) throw new ArgumentOutOfRangeException("R2RML Rule: Must have ONE TableName in LogicalTable.");
            Triple tableName = tableNameList.First();
            output.SetSourceName(((ILiteralNode)tableName.Object).Value);
        }

        private void MapToSubjectMap(IGraph graph, IEnumerable<Triple> smList, TriplesMap output)
        {
            if (smList.Count() != 1) throw new ArgumentOutOfRangeException("R2RML Rule: Must have ONE SubjectMap per TriplesMap.");
            Triple subjectMap = smList.First();

            IEnumerable<Triple> templateList = graph.GetTriplesWithSubjectPredicate(subjectMap.Object, graph.GetUriNode("rr:template"));
            if (templateList.Count() != 1) throw new ArgumentOutOfRangeException("R2RML Rule: Must have ONE Template in SubjectMap.");
            Triple template = templateList.First();
            output.SetTemplate(((ILiteralNode)template.Object).Value);

            IEnumerable<Triple> classTypeList = graph.GetTriplesWithSubjectPredicate(subjectMap.Object, graph.GetUriNode("rr:class"));
            if (classTypeList.Count() != 1) throw new ArgumentOutOfRangeException("R2RML Rule: Must have ONE class in SubjectMap.");
            Triple classType = classTypeList.First();

            string classNameQName;
            graph.NamespaceMap.ReduceToQName(((IUriNode)classType.Object).Uri.ToString(), out classNameQName);
            output.SetClassName(classNameQName);
        }

        private void MapToPredicateObjectMap(IGraph graph, IEnumerable<Triple> pomList, TriplesMap output)
        {
            foreach (Triple pom in pomList)
            {
                IEnumerable<Triple> predicateList = graph.GetTriplesWithSubjectPredicate(pom.Object, graph.GetUriNode("rr:predicate"));
                if (predicateList.Count() != 1) throw new ArgumentOutOfRangeException("R2RML Rule: Must have ONE Predicate in PredicateObjectMap.");
                Triple predicate = predicateList.First();
                PredicateMap predicateMap = new PredicateMap();
                string relationNameQName;
                graph.NamespaceMap.ReduceToQName(((IUriNode)predicate.Object).Uri.ToString(), out relationNameQName);
                predicateMap.SetRelationName(relationNameQName);

                IEnumerable<Triple> objectMapList = graph.GetTriplesWithSubjectPredicate(pom.Object, graph.GetUriNode("rr:objectMap"));
                if (objectMapList.Count() != 1) throw new ArgumentOutOfRangeException("WIP: Currently support ONE ObjectMap in PredicateObjectMap.");
                Triple objectMap = objectMapList.First();

                IEnumerable<Triple> columnList = graph.GetTriplesWithSubjectPredicate(objectMap.Object, graph.GetUriNode("rr:column"));
                if (columnList.Count() != 1) throw new ArgumentOutOfRangeException("R2RML Rule: Must have ONE column in ObjectMap.");
                Triple column = columnList.First();
                ObjectMap objMap = new ObjectMap();
                objMap.SetSourceName(((ILiteralNode)column.Object).Value);

                output.AddRelationObjectConfig(predicateMap, objMap);
            }
        }
    }
}
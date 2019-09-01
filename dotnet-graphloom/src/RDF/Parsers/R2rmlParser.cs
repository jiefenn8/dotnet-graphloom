using GraphLoom.Mappers.Exceptions;
using GraphLoom.Mappers.Rdf.R2rml;
using GraphLoom.Mappers.Rdf.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace GraphLoom.Mappers.Rdf.Parsers
{
    public class R2rmlParser
    {
        private readonly string _r2rmlPrefix = "rr";
        private IRdfLoader _rdfLoader;
        private TurtleParser _turtleParser;

        public R2rmlParser()
        {
            _rdfLoader = new RdfLoaderWrapper();
            _turtleParser = new TurtleParser();
        }

        public R2rmlParser(IRdfLoader rdfLoader)
        {
            _rdfLoader = rdfLoader;
            _turtleParser = new TurtleParser();
        }

        public R2rmlMap Parse(string filename)
        {
            return Parse(filename, null);
        }

        public R2rmlMap Parse(string filename, Uri baseUri)
        {
            IGraph graph = new Graph();
            graph.BaseUri = baseUri;
            graph = _rdfLoader.LoadFromFile(graph, filename, _turtleParser);

            return MapToR2RMLMap(graph);
        }

        private R2rmlMap MapToR2RMLMap(IGraph r2rmlGraph)
        {
            INamespaceMapper namespaceMap = r2rmlGraph.NamespaceMap;
            if (!namespaceMap.HasNamespace(_r2rmlPrefix)) { throw new ParserException("'rr' prefix uri not found."); }

            R2rmlMap r2rmlMap = new R2rmlMap(r2rmlGraph.BaseUri, r2rmlGraph.NamespaceMap); ;
            foreach (IUriNode tm in FindTriplesMaps(r2rmlGraph))
            {
                TriplesMap triplesMap = new TriplesMap(
                    MapToLogicalTable(FindLogicalTable(tm)),
                    MapToSubjectMap(FindSubjectMap(tm)));

                foreach(IBlankNode pom in FindPredicateObjectMaps(tm))
                {
                    Tuple<PredicateMap, ObjectMap> pomPair = MapToPredicateObjectMap(pom);
                    triplesMap.AddRelationNodePair(pomPair.Item1, pomPair.Item2);
                }
                r2rmlMap.AddEntityMap(triplesMap);
            }

            return r2rmlMap;
        }

        private IEnumerable<IUriNode> FindTriplesMaps(IGraph root)
        {
            IUriNode landmark = root.GetUriNode("rr:logicalTable");
            if(landmark == null) { throw new ParserException("No valid Triples Map with rr:logicalTable found."); }

            return root.GetTriplesWithPredicate(landmark)
                .Where((e) => e.Subject.NodeType.Equals(NodeType.Uri))
                .Select((s) => s.Subject)
                .Cast<IUriNode>();
        }

        private IEnumerable<IBlankNode> FindPredicateObjectMaps(IUriNode tmNode)
        {
            IUriNode predicateObjectMap = FindPredicateWithQName(tmNode, "rr:predicateObjectMap");

            return GetNodesWithSubjectPredicate(tmNode, predicateObjectMap, NodeType.Blank)
                .Cast<IBlankNode>();
        }

        private Tuple<PredicateMap, ObjectMap> MapToPredicateObjectMap(IBlankNode pomNode)
        {
            //todo: 6.3 : using the rr:predicateMap property, whose value MUST be a predicate map, or..
            IUriNode predicate = FindPredicateWithQName(pomNode, "rr:predicate");
            //todo: 6.3 : using the constant shortcut property rr:object.
            IUriNode objectMap = FindPredicateWithQName(pomNode, "rr:objectMap");
            IUriNode constPredicate = GetNodesWithSubjectPredicate(pomNode, predicate, NodeType.Uri)
                .Cast<IUriNode>()
                .First();

            IBlankNode objNode = GetNodesWithSubjectPredicate(pomNode, objectMap, NodeType.Blank)
                .Cast<IBlankNode>()
                .First();

            return new Tuple<PredicateMap, ObjectMap>(new PredicateMap(constPredicate), MapToObjectMap(objNode));
        }

        private ObjectMap MapToObjectMap(IBlankNode omNode)
        {
            IUriNode column = FindPredicateWithQName(omNode, "rr:column");
            string columnName = GetNodesWithSubjectPredicate(omNode, column, NodeType.Literal)
                .Cast<ILiteralNode>()
                .First()
                .Value;

            return new ObjectMap(columnName);
        }

        private IBlankNode FindLogicalTable(IUriNode tmNode)
        {
            IUriNode logicalTable = FindPredicateWithQName(tmNode, "rr:logicalTable");
            return GetNodesWithSubjectPredicate(tmNode, logicalTable, NodeType.Blank)
                .Cast<IBlankNode>()
                .First();
        }

        private LogicalTable MapToLogicalTable(IBlankNode ltNode)
        {
            IUriNode tableName = FindPredicateWithQName(ltNode, "rr:tableName");
            string source = GetNodesWithSubjectPredicate(ltNode, tableName, NodeType.Literal)
                .Cast<ILiteralNode>()
                .First()
                .Value;

            return new LogicalTable(source);
        }

        private IBlankNode FindSubjectMap(IUriNode tmNode)
        {
            IUriNode subjectMap = FindPredicateWithQName(tmNode, "rr:subjectMap");

            return GetNodesWithSubjectPredicate(tmNode, subjectMap, NodeType.Blank)
                .Cast<IBlankNode>()
                .First();
        }

        private SubjectMap MapToSubjectMap(IBlankNode smNode)
        {
            IUriNode template = FindPredicateWithQName(smNode, "rr:template");
            IUriNode classType = FindPredicateWithQName(smNode, "rr:class");

            IEnumerable<IUriNode> classes = GetNodesWithSubjectPredicate(smNode, classType, NodeType.Uri)
                .Cast<IUriNode>();

            string temp = GetNodesWithSubjectPredicate(smNode, template, NodeType.Literal)
                .Cast<ILiteralNode>()
                .First()
                .Value;

            SubjectMap subjectMap = new SubjectMap(temp);
            subjectMap.AddClasses(classes);

            return subjectMap;
        }

        //==================
        // Helper methods
        //==================

        //
        // Summary:
        //     Returns predicate from given node graph with a qname. Otherwise throw an exception.
        //
        // Parameters:
        //     subject:
        //       The subject to search within.     
        //     predciate: 
        //       The predciate to search for the node.
        //
        // Returns: 
        //     All nodes found that match subject and predicate. 
        private IEnumerable<INode> GetNodesWithSubjectPredicate(INode subject, INode predicate, NodeType type)
        {
            return subject.Graph
                .GetTriplesWithSubjectPredicate(subject, predicate)
                .Where((e) => e.Object.NodeType.Equals(type))
                .Select((s) => s.Object);
        }

        //
        // Summary:
        //     Returns predicate from given node graph with a qname. Otherwise throw an exception.
        //
        // Parameters:
        //     node:
        //       The node to search within.     
        //     qname: 
        //       The qname to search for the node.
        //
        // Returns: 
        //     The predicate Uri matching qname. Otherwise null if there is no match.
        private IUriNode FindPredicateWithQName(INode node, string qname)
        {
            IUriNode predicate = node.Graph.GetUriNode(qname);
            return predicate != null ? predicate : throw new ParserException(qname + "not found in root graph.");       
        }
    }
}
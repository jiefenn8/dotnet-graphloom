using GraphLoom.Mapper.Configuration;
using System;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF
{
    public class RDFGraphMapper
    {
        private RDFStatementsAssembler RdfAssembler;
        private IGraph OutputGraph;
        private bool Active = false;
        private bool Cancelled = false;

        private RDFGraphMapper() { }
        public RDFGraphMapper(RDFStatementsAssembler assembler)
        {
            this.RdfAssembler = assembler;
        }

        public virtual IGraph MapToGraph(IInputSource input, IMapperConfig config)
        {
            OutputGraph = new Graph();
          
            if (Active) throw new InvalidOperationException("Mapper already busy mapping.");
            else Active = true;

            if (input == null || config == null) return OutputGraph;

            ConfigurePrefixNamespaces(config.ListNamespaces());

            Cancelled = false;
            foreach (IStatementsConfig triplesMap in config.ListStatementsConfigs())
            {
                if (Cancelled)
                {
                    RdfAssembler.StopTask();
                    break;
                }
               OutputGraph.Merge(RdfAssembler.AssembleSubjectStatements(input, triplesMap, OutputGraph.NamespaceMap));
            }
            Active = false;

            return OutputGraph;
        }

        private void ConfigurePrefixNamespaces(IDictionary<string, string> prefixNsMap)
        {
            foreach(KeyValuePair<string, string> prefixNsPair in prefixNsMap)
            {
                OutputGraph.NamespaceMap.AddNamespace(prefixNsPair.Key, RDFUriFactory.FromString(prefixNsPair.Value));
            }
        }

        public virtual void StopTask()
        {
            Cancelled = true;
        }
    }
}

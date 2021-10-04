using System;
using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.Core;
using VDS.RDF;

namespace GraphLoom.Mapper
{
    public abstract class BaseGraphMapper<T> : IGraphMapper where T : IGenericGraph, new()
    {
        protected IStatementAssembler statementAssembler;
        protected bool cancelled = false;

        private BaseGraphMapper() { }

        public BaseGraphMapper(IStatementAssembler assembler)
        {
            statementAssembler = assembler;
        }

        public virtual IGenericGraph MapToGraph(IInputSource source, IMapperConfig config)
        {
            IGenericGraph OutputGraph = new T();

            cancelled = false;
            foreach (IStatementsConfig entityConfig in config.ListStatementsConfigs())
            {
                if (cancelled)
                {
                    statementAssembler.StopTask();
                    break;
                }
                OutputGraph.Merge(statementAssembler.AssembleEntityStatements(source, entityConfig, config.ListNamespaces()));
            }

            return OutputGraph;
        }

        public Type GetGraphType() => typeof(T);

        public virtual void StopTask() => cancelled = true;

        /// <inheritdoc/>
        public Graph MapToGraph(IInputSource source, IConfigMaps configMaps)
        {
            throw new NotImplementedException();
        }
    }
}

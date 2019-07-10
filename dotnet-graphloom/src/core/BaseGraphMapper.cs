using GraphLoom.Mapper.Configuration;
using System;

namespace GraphLoom.Mapper
{
    public abstract class BaseGraphMapper<T> : IGraphMapper where T : IGenericGraph, new()
    {
        protected IStatementAssembler StmtAssembler;
        protected bool Cancelled = false;

        private BaseGraphMapper() { }

        public BaseGraphMapper(IStatementAssembler assembler)
        {
            StmtAssembler = assembler;
        }

        public virtual IGenericGraph MapToGraph(IInputSource source, IMapperConfig config)
        {
            IGenericGraph OutputGraph = new T();

            Cancelled = false;
            foreach(IStatementsConfig entityConfig in config.ListStatementsConfigs())
            {
                if (Cancelled)
                {
                    StmtAssembler.StopTask();
                    break;
                }
                OutputGraph.Merge(StmtAssembler.AssembleEntityStatements(source, entityConfig, config.ListNamespaces()));
            }

            return OutputGraph;
        }

        public Type GetGraphType() => typeof(T);

        public virtual void StopTask() => Cancelled = true;
    }
}

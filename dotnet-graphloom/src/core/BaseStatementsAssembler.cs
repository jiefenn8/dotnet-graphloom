using GraphLoom.Mapper.Configuration;
using System.Collections.Generic;

namespace GraphLoom.Mapper
{
    public abstract class BaseStatementAssembler<T> : IStatementAssembler where T : IGenericGraph, new()
    {
        protected bool Cancelled = false;
        protected T SubjectGraph;

        public abstract IGenericGraph AssembleEntityStatements(IInputSource source, IStatementsConfig entityConfig, IDictionary<string, string> nsMap);

        public virtual void StopTask() => Cancelled = true;
    }
}

using GraphLoom.Mapper.Configuration;
using System.Collections.Generic;

namespace GraphLoom.Mapper
{
    public interface IStatementAssembler
    {
        IGenericGraph AssembleEntityStatements(IInputSource source, IStatementsConfig config, IDictionary<string, string> ns);
        void StopTask();
    }
}

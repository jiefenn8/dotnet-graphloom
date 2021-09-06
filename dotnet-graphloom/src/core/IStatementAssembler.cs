using System.Collections.Generic;
using GraphLoom.Mapper.Configuration;

namespace GraphLoom.Mapper
{
    public interface IStatementAssembler
    {
        IGenericGraph AssembleEntityStatements(IInputSource source, IStatementsConfig config, IDictionary<string, string> ns);
        void StopTask();
    }
}

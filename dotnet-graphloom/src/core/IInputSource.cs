using System;
using System.Collections.Generic;
using GraphLoom.Mapper.Core.InputSource;

namespace GraphLoom.Mapper
{
    public interface IInputSource
    {
        [Obsolete("This method is deprecated and will be removed in the future.")]
        List<IDictionary<string, string>> GetEntityRecords(string entity);
        void ExecuteEntityQuery(IEntityReference entityRef, Action<IEntityResult> action);
    }
}

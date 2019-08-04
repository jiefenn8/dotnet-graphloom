using System.Collections.Generic;

namespace GraphLoom.Mapper
{
    public interface IInputSource
    {
        List<IDictionary<string, string>> GetEntityRecords(string entity);
    }
}

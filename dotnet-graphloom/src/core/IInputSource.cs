using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLoom.Mapper
{
    public interface IInputSource
    {
        List<IDictionary<string, string>> GetEntityRecords(string entity);
    }
}

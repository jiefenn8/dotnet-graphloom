using System.Collections.Generic;
using GraphLoom.Mapper.Core.ConfigMaps;

namespace GraphLoom.Mapper.Core
{
    public interface IConfigMaps : IEnumerable<IEntityMap>
    {
        Dictionary<string, string> GetNamespaceMap();
        ISet<IEntityMap> GetEntityMaps();
    }
}

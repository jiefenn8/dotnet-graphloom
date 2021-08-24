using GraphLoom.Mapper.Core.ConfigMaps;
using System.Collections.Generic;

namespace GraphLoom.Mapper.Core
{
    public interface IConfigMaps : IEnumerable<IEntityMap>
    {
        Dictionary<string, string> GetNamespaceMap();
        ISet<IEntityMap> GetEntityMaps();
    }
}

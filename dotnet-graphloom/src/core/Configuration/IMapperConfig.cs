using System.Collections.Generic;

namespace GraphLoom.Mapper.Configuration
{
    public interface IMapperConfig
    {
        IDictionary<string, string> ListNamespaces();
        void AddNamespace(string prefix, string ns);
        List<IStatementsConfig> ListStatementsConfigs();
        void AddStatementsConfig(IStatementsConfig config);
    }
}

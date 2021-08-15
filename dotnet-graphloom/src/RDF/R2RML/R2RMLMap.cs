using GraphLoom.Mapper.Configuration;
using System.Collections.Generic;

namespace GraphLoom.Mapper.RDF.R2RML
{
    public class R2RMLMap : IMapperConfig
    {
        private IDictionary<string, string> _namespaceMap = new Dictionary<string, string>();
        private List<IStatementsConfig> _statementsConfigs = new List<IStatementsConfig>();

        public IDictionary<string, string> ListNamespaces()
        {
            return _namespaceMap;
        }

        public void AddNamespace(string prefix, string ns)
        {
            _namespaceMap.Add(prefix, ns);
        }

        public List<IStatementsConfig> ListStatementsConfigs()
        {
            return _statementsConfigs;
        }

        public void AddStatementsConfig(IStatementsConfig config)
        {
            _statementsConfigs.Add(config);
        }
    }
}

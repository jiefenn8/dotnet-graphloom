using GraphLoom.Mapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLoom.Mapper.RDF.R2RML
{
    public class R2RMLMap : IMapperConfig
    {
        private IDictionary<string, string> NamespaceMap = new Dictionary<string, string>();
        private List<IStatementsConfig> StatementsConfigs = new List<IStatementsConfig>();

        public IDictionary<string, string> ListNamespaces()
        {
            return NamespaceMap;
        }

        public void AddNamespace(string prefix, string ns)
        {
            NamespaceMap.Add(prefix, ns);
        }

        public List<IStatementsConfig> ListStatementsConfigs()
        {
            return StatementsConfigs;
        }

        public void AddStatementsConfig(IStatementsConfig config)
        {
            StatementsConfigs.Add(config);
        }
    }
}

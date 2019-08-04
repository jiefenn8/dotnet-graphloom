using GraphLoom.Mapper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLoom.Mapper.RDF.Configuration
{
    public class TriplesMap : IStatementsConfig
    {
        private IDictionary<IRelationConfig, IObjectsConfig> _predicateObjectMaps = new Dictionary<IRelationConfig, IObjectsConfig>();
        private string _className;
        private string _sourceName;
        private string _template;

        public void AddRelationObjectConfig(IRelationConfig relationConfig, IObjectsConfig objectsConfig)
        {
            _predicateObjectMaps.Add(relationConfig, objectsConfig);
        }

        public string GetClassName()
        {
            return _className;
        }

        public IDictionary<IRelationConfig, IObjectsConfig> GetRelationObjectConfigs()
        {
            return _predicateObjectMaps;
        }

        public string GetSourceName()
        {
            return _sourceName;
        }

        public string GetTemplate()
        {
            return _template;
        }

        public void SetClassName(string className)
        {
            _className = className;
        }

        public void SetSourceName(string source)
        {
            _sourceName = source;
        }

        public void SetTemplate(string template)
        {
            _template = template;
        }
    }
}

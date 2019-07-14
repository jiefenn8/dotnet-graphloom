using GraphLoom.Mapper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLoom.Mapper.RDF.Configuration
{
    public class TriplesMap : IStatementsConfig
    {
        private IDictionary<IRelationConfig, IObjectsConfig> PredicateObjectMaps = new Dictionary<IRelationConfig, IObjectsConfig>();
        private string ClassName;
        private string SourceName;
        private string Template;

        public void AddRelationObjectConfig(IRelationConfig relationConfig, IObjectsConfig objectsConfig)
        {
            PredicateObjectMaps.Add(relationConfig, objectsConfig);
        }

        public string GetClassName()
        {
            return ClassName;
        }

        public IDictionary<IRelationConfig, IObjectsConfig> GetRelationObjectConfigs()
        {
            return PredicateObjectMaps;
        }

        public string GetSourceName()
        {
            return SourceName;
        }

        public string GetTemplate()
        {
            return Template;
        }

        public void SetClassName(string className)
        {
            ClassName = className;
        }

        public void SetSourceName(string source)
        {
            SourceName = source;
        }

        public void SetTemplate(string template)
        {
            Template = template;
        }
    }
}

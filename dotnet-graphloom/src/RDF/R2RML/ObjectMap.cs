using GraphLoom.Mapper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLoom.Mapper.RDF.R2RML
{
    public class ObjectMap : IObjectsConfig
    {
        private string _sourceName;
        public string GetSourceName()
        {
            return _sourceName;
        }

        public void SetSourceName(string source)
        {
            _sourceName = source;
        }
    }
}

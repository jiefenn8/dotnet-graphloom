using GraphLoom.Mapper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLoom.Mapper.RDF.R2RML
{
    public class ObjectMap : IObjectsConfig
    {
        private string SourceName;
        public string GetSourceName()
        {
            return SourceName;
        }

        public void SetSourceName(string source)
        {
            SourceName = source;
        }
    }
}

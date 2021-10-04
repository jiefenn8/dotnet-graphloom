using System;
using System.Collections.Generic;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF
{
    public class NamespaceHelper
    {
        public static INamespaceMapper ToIGraphNamespaceMap(IDictionary<string, string> namespaceMap)
        {
            INamespaceMapper rdfNamespaceMap = new NamespaceMapper();
            foreach (KeyValuePair<string, string> nsPair in namespaceMap)
            {
                rdfNamespaceMap.AddNamespace(nsPair.Key, new Uri(nsPair.Value));
            }
            return rdfNamespaceMap;
        }

        public static IDictionary<string, string> ToStringDictionary(INamespaceMapper rdfNamespaceMap)
        {
            IDictionary<string, string> namespaceMap = new Dictionary<string, string>();
            foreach (string prefix in rdfNamespaceMap.Prefixes)
            {
                namespaceMap.Add(prefix, rdfNamespaceMap.GetNamespaceUri(prefix).ToString());
            }
            return namespaceMap;
        }
    }
}

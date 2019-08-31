using GraphLoom.Mapper.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF.R2RML
{
    //
    // Summary:
    //     Implementation of R2RML with ConfigMaps interface. 
    //     This class stores and manages all the TriplesMap associated 
    //     with a mapping document. 
    public class R2RMLMap : IConfigMaps
    {
        private List<IEntityMap> _entityMaps = new List<IEntityMap>();

        public INamespaceMapper NamespaceMapper { get; }

        public Uri BaseUri { get; }

        public R2RMLMap(Uri baseUri, INamespaceMapper namespaceMap)
        {
            BaseUri = baseUri;
            NamespaceMapper = new NamespaceMapper();
            if (namespaceMap != null) { NamespaceMapper.Import(namespaceMap); }  
        }

        public void AddEntityMap(IEntityMap config)
        {
            _entityMaps.Add(config);
        }

        public IReadOnlyCollection<IEntityMap> ListEntityMaps()
        {
            return new ReadOnlyCollection<IEntityMap>(_entityMaps);
        }
    }
}

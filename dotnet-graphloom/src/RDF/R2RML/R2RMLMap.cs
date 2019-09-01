using GraphLoom.Mappers.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VDS.RDF;

namespace GraphLoom.Mappers.Rdf.R2rml
{
    //
    // Summary:
    //     Implementation of R2RML with ConfigMaps interface. 
    //     This class stores and manages all the TriplesMap associated 
    //     with a mapping document. 
    public class R2rmlMap : IConfigMaps
    {
        private List<IEntityMap> _entityMaps = new List<IEntityMap>();

        public INamespaceMapper NamespaceMapper { get; }

        public Uri BaseUri { get; }

        public R2rmlMap(Uri baseUri, INamespaceMapper namespaceMap)
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

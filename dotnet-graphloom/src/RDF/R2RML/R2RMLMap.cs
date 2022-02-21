//-----------------------------------------------------------------------
// <copyright file="R2RMLMap.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.Core;
using GraphLoom.Mapper.Core.ConfigMaps;

namespace GraphLoom.Mapper.RDF.R2RML
{
    /// <summary>
    /// Implementation of R2RMLMap with <see cref="IMapperConfig"/> and
    /// <see cref="IConfigMaps"/> omterfaces. 
    /// This class is an immutable class and requires the use of its builder
    /// class to populate and create an instance. 
    /// </summary>
    public class R2RMLMap : IMapperConfig, IConfigMaps
    {
        /// <summary>
        /// Collection of prefix namespaces.
        /// </summary>
        private readonly IDictionary<string, string> prefixNamespace;

        /// <summary>
        /// Collection of triples map associated with this R2RML map.
        /// To be deprecated.
        /// </summary>
        private readonly List<IStatementsConfig> _statementsConfigs = new List<IStatementsConfig>();

        /// <summary>
        /// Collection of triples map associated with this R2RML map.
        /// </summary>
        private readonly ISet<IEntityMap> triplesMaps;

        /// <summary>
        /// Previous default constructor. 
        /// To be deprecated. 
        /// </summary>
        public R2RMLMap() 
        { 
            prefixNamespace = new Dictionary<string, string>();
            triplesMaps = new HashSet<IEntityMap>();   
        }

        /// <summary>
        /// Constructs a R2RMLMap with the specified Builder containing the
        /// properties to populate and initialise this immutable instance. 
        /// </summary>
        /// <param name="builder">the r2rml map builder to build from</param>
        public R2RMLMap(Builder builder)
        {
            prefixNamespace = builder.NsPrefixMap;
            triplesMaps = builder.TriplesMaps;
        }

        /// <inheritdoc/>
        public IDictionary<string, string> ListNamespaces()
        {
            return prefixNamespace;
        }

        /// <inheritdoc/>
        public void AddNamespace(string prefix, string ns)
        {
            prefixNamespace.Add(prefix, ns);
        }

        /// <inheritdoc/>
        public List<IStatementsConfig> ListStatementsConfigs()
        {
            return _statementsConfigs;
        }

        /// <inheritdoc/>
        public void AddStatementsConfig(IStatementsConfig config)
        {
            _statementsConfigs.Add(config);
        }

        /// <inheritdoc/>
        public IDictionary<string, string> GetNamespaceMap()
        {
            return prefixNamespace;
        }

        /// <inheritdoc/>
        public ISet<IEntityMap> GetEntityMaps()
        {
            return triplesMaps;
        }

        /// <inheritdoc/>
        public IEnumerator<IEntityMap> GetEnumerator()
        {
            return triplesMaps.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Builder class for <see cref="R2RMLMap"/>
        /// </summary>
        public class Builder
        {
            /// <summary>
            /// R2RML prefix.
            /// </summary>
            private readonly string RrPrefix = "rr";

            /// <summary>
            /// Gets the namespace prefix map.
            /// </summary>
            public IDictionary<string, string> NsPrefixMap { get; private set; } = new Dictionary<string, string>();
            
            /// <summary>
            /// Gets the collection of triples map associated with this R2RML map.
            /// </summary>
            public ISet<IEntityMap> TriplesMaps { get; private set; } = new HashSet<IEntityMap>();

            /// <summary>
            /// Adds a namespace to given prefix to this R2RML map.
            /// </summary>
            /// <param name="prefix">the given prefix</param>
            /// <param name="prefixNamespace">the namespace to associate the prefix with</param>
            /// <returns>this builder for fluent method chaining</returns>
            public Builder AddNsPrefix(string prefix, string prefixNamespace)
            {
                NsPrefixMap.Add(prefix, prefixNamespace);
                return this;
            }

            /// <summary>
            /// Adds a triples map to this R2RML map.
            /// </summary>
            /// <param name="triplesMap">the triples map to add</param>
            /// <returns>this builder for fluent method chaining</returns>
            public Builder AddTriplesMap(IEntityMap triplesMap)
            {
                TriplesMaps.Add(triplesMap);
                return this;
            }

            /// <summary>
            /// Returns an inmmutable instance of R2RML map containing the properties
            /// given to its builder.
            /// </summary>
            /// <returns>instance of R2RML map created with the info in this builder</returns>
            public R2RMLMap Build()
            {
                string value; 
                if(!NsPrefixMap.TryGetValue(RrPrefix, out value))
                {
                    if(value is null)
                    {
                        NsPrefixMap.Add(RrPrefix, "http://www.w3.org/ns/r2rml#");
                    }
                }

                return new R2RMLMap(this);
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="TriplesMapFuture.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using GraphLoom.Mapper.Core.ConfigMaps;
using GraphLoom.Mapper.Core.InputSource;
using NLog;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF.R2RML
{
    /// <summary>
    /// Implementation of R2RML TriplesMap with <see cref="IEntityMap"/> interface. 
    /// This class is an immutable class and requires the use of its builder
    /// class to populate and create an instance.
    /// </summary>
    public class TriplesMapFuture : IEntityMap
    {
        /// <summary>
        /// Id name of this triples map instance.
        /// </summary>
        private readonly string idName;

        /// <summary>
        /// Logical table associated with this instance.
        /// </summary>
        private readonly LogicalTable logicalTable;

        /// <summary>
        /// Subject map associated with this instance.
        /// </summary>
        private readonly IPropertyMap subjectMap;

        /// <summary>
        /// Collection of predicate object maps associated with this instance.
        /// </summary>
        private readonly IDictionary<IRelationMap, INodeMap> predicateObjectMaps;

        /// <summary>
        /// Collection of predicate referencing object maps associated with this instance.
        /// </summary>
        private readonly IDictionary<IRelationMap, INodeMap> predicateRefObjectMaps;

        /// <summary>
        /// Constructs a TriplesMap with the specified Builder containing the
        /// properties to populate and initialise this immutable instance.
        /// </summary>
        /// <param name="builder">the triples map builder to build from</param>
        protected TriplesMapFuture(Builder builder)
        {
            _ = builder ?? throw new ArgumentNullException();
            idName = builder.IdName;
            logicalTable = builder.LogicalTable;
            subjectMap = builder.SubjectMap;
            predicateObjectMaps = builder.PredicateObjectMaps;
            predicateRefObjectMaps = builder.PredicateRefObjectMaps;
        }

        /// <inheritdoc/>
        public IGraph GenerateClassTerms(IUriNode term)
        {
            IGraph model = new Graph();
            subjectMap.ListEntityClasses().ForEach((c) =>
            {
                model.Assert(term, model.CreateUriNode("rdf:type"), c);
            });

            return model;
        }

        /// <inheritdoc/>
        public IUriNode GenerateEntityTerm(IEntity entity)
        {
            return subjectMap.GenerateEntityTerm(entity);
        }

        /// <inheritdoc/>
        public IGraph GenerateNodeTerms(IUriNode term, IEntity entity)
        {
            IGraph model = new Graph();
            foreach (var item in predicateObjectMaps.Keys)
            {
                INodeMap objectMap = predicateObjectMaps[item];
                INode node = objectMap.GenerateNodeTerm(entity);
                if (!(node is null))
                {
                    model.Assert(term, item.GenerateRelationTerm(entity), objectMap.GenerateNodeTerm(entity));
                }
            }

            return model;
        }

        /// <inheritdoc/>
        public IGraph GenerateRefNodeTerms(IUriNode term, IInputSource source)
        {
            IGraph model = new Graph();
            foreach (var item in predicateRefObjectMaps.Keys)
            {
                INodeMap refObjectMap = predicateRefObjectMaps[item];
                ISourceResult jointLogicalTable = logicalTable.AsJointLogicalTable((RefObjectMap)refObjectMap);
                jointLogicalTable.ForEachEntity(source, (e) =>
                {
                    INode node = refObjectMap.GenerateNodeTerm(e);
                    if (!(node is null))
                    {
                        model.Assert(term, item.GenerateRelationTerm(e), node);
                    }
                });
            }

            return model;
        }

        /// <inheritdoc/>
        public string getIdName()
        {
            return idName;
        }

        /// <inheritdoc/>
        public ISourceResult GetSourceResult()
        {
            return logicalTable;
        }

        /// <inheritdoc/>
        public bool HasNodePairs()
        {
            return predicateObjectMaps.Any();
        }

        /// <summary>
        /// Builder class for <see cref="TriplesMapFuture"/>.
        /// </summary>
        public class Builder
        {
            /// <summary>
            /// Logger for this class builder.
            /// </summary>
            private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

            /// <summary>
            /// Gets the id name of this triples map.
            /// </summary>
            public string IdName { get; private set; }

            /// <summary>
            /// Gets the logical table associated with this triples map.
            /// </summary>
            public LogicalTable LogicalTable { get; private set; }

            /// <summary>
            /// Gets the subject map associated with this triples map.
            /// </summary>
            public IPropertyMap SubjectMap { get; private set; }

            /// <summary>
            /// Gets the collection of object maps associated with a relation map.
            /// </summary>
            public IDictionary<IRelationMap, INodeMap> PredicateObjectMaps { get; } = new Dictionary<IRelationMap, INodeMap>();

            /// <summary>
            /// Gets the collection of reference object maps associated with a relation map.
            /// </summary>
            public IDictionary<IRelationMap, INodeMap> PredicateRefObjectMaps { get; } = new Dictionary<IRelationMap, INodeMap>();

            /// <summary>
            /// Constructs a TriplesMap Builder with the specified triples map id,
            /// logical table and subject map instance.
            /// </summary>
            /// <param name="idName">the name of this triples map definition</param>
            /// <param name="logicalTable">the logical table to set on this triples map</param>
            /// <param name="subjectMap">the subject map to set on this triples map</param>
            public Builder(string idName, LogicalTable logicalTable, IPropertyMap subjectMap)
            {
                IdName = idName ?? throw new ArgumentNullException("ID name must not be null.");
                LogicalTable = logicalTable ?? throw new ArgumentNullException("Logical table must not be null.");
                SubjectMap = subjectMap ?? throw new ArgumentNullException("Subject map must not be null.");
            }

            /// <summary>
            /// Adds a predicate map and object map pair to this triples map.
            /// </summary>
            /// <param name="pom">the pair to add to triples-map</param>
            /// <returns>this builder for fluent method chaining</returns>
            public Builder AddPredicateObjectMap(KeyValuePair<IRelationMap, INodeMap> pom)
            {
                INodeMap nodeMap = pom.Value;
                if (nodeMap is RefObjectMap)
                {
                    PredicateRefObjectMaps.Add(pom.Key, nodeMap);
                    return this;
                }

                PredicateObjectMaps.Add(pom.Key, nodeMap);
                return this;
            }

            /// <summary>
            /// Returns an immutable instance of triples maps containing the properties
            /// given to its builder.
            /// </summary>
            /// <returns>instance of triples map created with the info in this builder</returns>
            public TriplesMapFuture Build()
            {
                LOGGER.Debug("Building TriplesMap from parameters. ID:{}", IdName);
                TriplesMapFuture triplesMap = new TriplesMapFuture(this);
                LOGGER.Debug("{}", triplesMap);
                return triplesMap;
            }
        }
    }
}

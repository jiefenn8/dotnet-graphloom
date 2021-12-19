//-----------------------------------------------------------------------
// <copyright file="RefObjectMap.cs" company="github.com/jiefenn8">
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
using GraphLoom.Mapper.Util;
using NLog;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF.R2RML
{
    /// <summary>
    /// Implementation of R2RML RefObjectMap with <see cref="INodeMap"/> interface.
    /// </summary>
    public class RefObjectMap : INodeMap, IUniqueId
    {
        /// <summary>
        /// Unique indentifier for this instance. 
        /// </summary>
        private readonly Guid guid;

        /// <summary>
        /// Parent triples map associated with this instance.
        /// </summary>
        public IEntityMap ParentTriplesMap { get; private set; }

        /// <summary>
        /// Collection of the join conditions associated with this instance.
        /// </summary>
        public ISet<JoinCondition> JoinConditions { get; private set; }

        /// <summary>
        /// Constructs a RefObjectMap with the specified Builder containing the
        /// properties to populate and initialise an immutable instance.
        /// </summary>
        /// <param name="builder">the ref object map builder to build from</param>
        private RefObjectMap(Builder builder)
        {
            _ = builder ?? throw new ArgumentNullException();
            guid = builder.Guid;
            ParentTriplesMap = builder.ParentTriplesMap;
            JoinConditions = builder.JoinConditions;
        }

        /// <inheritdoc/>
        public INode GenerateNodeTerm(IEntity entity)
        {
            return ParentTriplesMap.GenerateEntityTerm(entity);
        }

        /// <summary>
        /// Returns true if this ref object map contains any join conditions.
        /// </summary>
        /// <returns>true if there is any join conditions</returns>
        public bool HasJoinConditions()
        {
            return JoinConditions.Any();
        }

        /// <summary>
        /// Returns true if the queries of the logical tables from both the parent
        /// triples map and the triples map that called this is equal.
        /// </summary>
        /// <param name="logicalTable">the logical table of the calling triples map</param>
        /// <returns>true if queries from both are equal</returns>
        protected bool IsQueryEqual(LogicalTable logicalTable)
        {
            return ParentTriplesMap.GetSourceResult().Equals(logicalTable);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }

        /// <inheritdoc/>
        public string GetUniqueId()
        {
            return guid.ToString();
        }

        /// <summary>
        /// Builder class for <see cref="RefObjectMap"/>.
        /// </summary>
        public class Builder
        {
            /// <summary>
            /// Logger for this class builder. 
            /// </summary>
            private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

            /// <summary>
            /// The unique indentifier for the reference object map to be created. 
            /// </summary>
            public Guid Guid { get; private set; }

            /// <summary>
            /// Gets the parent triples map associated.
            /// </summary>
            public IEntityMap ParentTriplesMap { get; private set; }

            /// <summary>
            /// Gets the collection of join conditions associated.
            /// </summary>
            public ISet<JoinCondition> JoinConditions { get; } = new HashSet<JoinCondition>();

            /// <summary>
            /// Constructs a RefObjectMap Builder with the specified triples map and
            /// join condition to this instance.
            /// </summary>
            /// <param name="parentTriplesMap">the triples map that this instance refers to</param>
            public Builder(IEntityMap parentTriplesMap)
            {
                ParentTriplesMap = parentTriplesMap ?? throw new ArgumentNullException(nameof(parentTriplesMap));
            }

            /// <summary>
            /// Adds a join condition to this reference object map that associates
            /// two queries from the logical tables of both parent triples map and
            /// this triples map(that accessed this ref object map) by the given
            /// columns that exists in their table.
            /// </summary>
            /// <param name="parent">the column in the parent triples maps' logical table</param>
            /// <param name="child">the column in this triples map's logical table</param>
            /// <returns>this builder for fluent method chaining</returns>
            public Builder AddJoinCondition(string parent, string child)
            {
                LOGGER.Debug("Creating JoinCondition.");
                JoinCondition joinCondition = new JoinCondition(parent, child);
                LOGGER.Debug("{}", joinCondition);
                JoinConditions.Add(joinCondition);

                return this;
            }

            /// <summary>
            /// Returns an immutable instance of ref object map containing the
            /// properties given to its builder.
            /// </summary>
            /// <returns>the ref object map created with this builder parameters</returns>
            public RefObjectMap Build()
            {
                Guid = Guid.NewGuid();
                LOGGER.Debug("Building RefObjectMap from parameters. GUID: {}", Guid);
                RefObjectMap refObjectMap = new RefObjectMap(this);
                LOGGER.Debug("{}", refObjectMap);

                return refObjectMap;
            }
        }
    }
}

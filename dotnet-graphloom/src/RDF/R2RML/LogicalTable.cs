//-----------------------------------------------------------------------
// <copyright file="LogicalTable.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using GraphLoom.Mapper.Core.ConfigMaps;
using GraphLoom.Mapper.Core.InputSource;
using GraphLoom.Mapper.Exceptions;
using GraphLoom.Mapper.Util;
using Newtonsoft.Json;

namespace GraphLoom.Mapper.RDF.R2RML
{
    /// <summary>
    /// Implementation of R2RML LogicalTable with <see cref="ISourceResult"/> interface.
    /// </summary>
    public class LogicalTable : ISourceResult, IUniqueId
    {
        /// <summary>
        /// Class logger.
        /// </summary>
        private static readonly NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Unique indentifier for this instance.
        /// </summary>
        private readonly Guid guid; 

        /// <summary>
        /// Entity reference containing the query configuration.
        /// </summary>
        private readonly IEntityReference entityReference;

        /// <summary>
        /// Constructs a LogicalTable with the specified Builder containing the
        /// properties to populate and initialise an immutable instance.
        /// </summary>
        /// <param name="builder">the logical table builder to build from</param>
        private LogicalTable(Builder builder) 
        {
            guid = builder.Guid;
            entityReference = builder.EntityReference;
        }

        /// <summary>
        /// Retrieves the LogicalTable from the parent TriplesMap found in the given
        /// RefObjectMap.Returns a LogicalTable a Joint SQL query that is the
        /// combination of this instance and the given LogicalTable.
        /// </summary>
        /// <param name="refObjectMap">the reference containing the parent TriplesMap</param>
        /// <returns>the LogicalTable of two Joint SQL tables</returns>
        public LogicalTable AsJointLogicalTable(RefObjectMap refObjectMap)
        {
            LogicalTable logicalTable = refObjectMap.GetParentTriplesMap().GetSourceMap();
            return new Builder(this).WithJointQuery(logicalTable, refObjectMap.ListJoinConditions()).Build();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is LogicalTable table &&
                   EqualityComparer<IEntityReference>.Default.Equals(entityReference, table.entityReference);
        }

        /// <inheritdoc/>
        public IEntityReference GetEntityReference()
        {
            return entityReference;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(entityReference);
        }

        /// <inheritdoc/>
        public string GetUniqueId()
        {
            return guid.ToString();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Builder class for LogicalTable.
        /// </summary>
        public class Builder
        {
            /// <summary>
            /// Gets the unique indentifier for the logical table.
            /// </summary>
            public Guid Guid { get; private set; }

            /// <summary>
            /// Gets the entity reference for the logical table. 
            /// </summary>
            public IEntityReference EntityReference { get; private set; }

            /// <summary>
            /// Constructs a Builder with the specified SourceConfig instance.
            /// </summary>
            /// <param name="entityReference">the query config to set on this logical table</param>
            public Builder(IEntityReference entityReference)
            {
                EntityReference = entityReference ?? throw new ArgumentNullException();
            }

            /// <summary>
            /// Constructs a Builder with the specified LogicalTable containing the
            /// query config instance.
            /// </summary>
            /// <param name="logicalTable">the logical table with the query config 
            /// needed to set on this logical table</param>
            public Builder(LogicalTable logicalTable)
            {
                EntityReference = logicalTable.entityReference;
            }

            /// <summary>
            /// Builds a query config with a join query consisting of two query,
            /// table or mixed that is associated to each other through join
            /// conditions.
            /// </summary>
            /// <param name="logicalTable">the second query or table to build a joint query</param>
            /// <param name="joinConditions">the set of joins conditions to use</param>
            /// <returns>this builder for fluent method chaining</returns>
            /// <exception cref="ParserException">If there no join conditions to build the query</exception>
            public Builder WithJointQuery(LogicalTable logicalTable, ISet<JoinCondition> joinConditions)
            {
                try
                {
                    string jointQuery = "SELECT child.* FROM " + PrepareQuery(EntityReference) + " AS child, ";
                    jointQuery += PrepareQuery(logicalTable.entityReference) + " AS parent";
                    jointQuery += " WHERE " + BuildJoinsRecursively(joinConditions.GetEnumerator());
                    string parentVersion = EntityReference.GetProperty("sqlVersion");
                    EntityReference = new R2RMLView.Builder(jointQuery, parentVersion).Build();
                } 
                catch(InvalidOperationException ex)
                {
                    throw new ParserException("Expected JoinConditions with joint query creation.", ex);
                }

                return this;
            }

            /// <summary>
            /// Recursively build all join conditions and return result as String.
            /// </summary>
            /// <param name="enumerator">to retrieve each join conditions</param>
            /// <returns>string of all join conditions</returns>
            /// <exception cref="InvalidOperationException">If enumerator has not elements</exception>
            private string BuildJoinsRecursively(IEnumerator<JoinCondition> enumerator)
            {
                if(!enumerator.MoveNext())
                {
                    throw new InvalidOperationException("Enumerator has no more elements.");
                }

                JoinCondition joinCondition = enumerator.Current;
                string joins = joinCondition.GetJoinString();
                if (enumerator.MoveNext())
                {
                    joins += "AND" + BuildJoinsRecursively(enumerator);
                }

                return joins;
            }

            /// <summary>
            /// Returns a prepared query using the query/table in the given source
            /// config.If the query config is a r2rml view, wrap the query before
            /// returning it.
            /// </summary>
            /// <param name="entityReference">the config to containing the query</param>
            /// <returns>the query prepared for further manipulation</returns>
            private string PrepareQuery(IEntityReference entityReference)
            {
                if(entityReference is R2RMLView)
                {
                    return "(" + entityReference.GetPayload() + ")";
                }

                return entityReference.GetPayload();
            }

            /// <summary>
            /// Returns an immutable instance of logical table containing the properties
            /// given to its builder.
            /// </summary>
            /// <returns>instance of logical table created with the info in this builder</returns>
            public LogicalTable Build()
            {
                Guid = Guid.NewGuid();
                LOGGER.Debug("Building logical table from parameters. ID:{}", Guid);
                LogicalTable logicalTable = new LogicalTable(this);
                LOGGER.Debug("{}", logicalTable);
                return logicalTable;
            }
        }
    }
}

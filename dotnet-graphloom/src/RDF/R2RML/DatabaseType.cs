//-----------------------------------------------------------------------
// <copyright file="DatabaseType.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using GraphLoom.Mapper.Core.InputSource;

namespace GraphLoom.Mapper.RDF.R2RML
{
    /// <summary>
    /// This interface extends the base class to manage the type of payload
    /// the query or reference can be from <see cref="PayloadType"/>.
    /// </summary>
    public class DatabaseType : PayloadType
    {

        /// <summary>
        /// The type for when the payload is a query.
        /// </summary>
        public static readonly DatabaseType QUERY = new DatabaseType("QUERY");

        /// <summary>
        /// The type for when the payload is a table name.
        /// </summary>
        public static readonly DatabaseType TABLE_NAME = new DatabaseType("TABLE_NAME");

        /// <inheritdoc/>
        public DatabaseType(string value) : base(value) { }
    }
}

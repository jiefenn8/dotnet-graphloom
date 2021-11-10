//-----------------------------------------------------------------------
// <copyright file="BaseTableOrView.cs" company="github.com/jiefenn8">
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
    /// This class extends the base methods of the <see cref="BaseEntityReference"/>
    /// to manage configuration to only handle SQL retrieval through the use of base
    /// table or view  name.
    /// </summary>
    public class BaseTableOrView : BaseEntityReference
    {
        /// <summary>
        /// Constructs a BaseTableOrView with the specified SQL table or view name
        /// as the source payload.
        /// </summary>
        /// <param name="payload">the table or view name</param>
        public BaseTableOrView(string payload) : base(payload, DatabaseType.TABLE_NAME, string.Empty) { }
    }
}

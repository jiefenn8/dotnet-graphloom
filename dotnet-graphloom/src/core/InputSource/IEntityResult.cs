//-----------------------------------------------------------------------
// <copyright file="IEntityResult.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

namespace GraphLoom.Mapper.Core.InputSource
{
    /// <summary>
    /// This interface defines the common base methods in handling the result
    /// containing any relevant entity of a query.Implementations of this interface
    /// should use the adapter pattern to wrap existing data source implementation
    /// with their data retrieval methods; And expose them through this API without
    /// the need of additional data store in memory.
    /// </summary>
    public interface IEntityResult
    {
        /// <summary>
        /// Returns true if the result iterator has an entity next. If there is
        /// no entity next in the iterator; Returns fae.
        /// </summary>
        /// <returns>true if there is an entity next, otherwise false</returns>
        bool HasNext();

        /// <summary>
        /// Returns the next entity in the result iterator.
        /// </summary>
        /// <returns>the next entity on the iterator</returns>
        IEntity NextEntity();
    }
}

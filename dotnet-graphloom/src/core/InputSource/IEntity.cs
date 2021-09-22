//-----------------------------------------------------------------------
// <copyright file="IEntity.cs" company="github.com/jiefenn8">
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
    /// This interface defines the common base methods in handling an entity 
    /// and its properties.Implementations of this interface should use the 
    /// adapter pattern to wrap existing data source implementation with their
    /// data retrieval methods; And expose them through this API without the 
    /// need of additional data store in memory.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Returns the value of an entity property. If the property or
        /// value does not exist; Returns null.
        /// </summary>
        /// <param name="key">the name of the entity property</param>
        /// <returns>the value of the given property name, otherwise null</returns>
        string GetPropertyValue(string key);
    }
}

//-----------------------------------------------------------------------
// <copyright file="IEntityReference.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using GraphLoom.Mapper.src.Core.InputSource;

namespace GraphLoom.Mapper.Core.InputSource
{
    /// <summary>
    /// This interface defines the base methods to manage the handling of
    /// payload and its related properties for consumption by the input
    /// source.
    /// </summary>
    public interface IEntityReference
    {
        /// <summary>
        /// Returns the payload type of this class.
        /// </summary>
        /// <returns>the payload type</returns>
        PayloadType GetPayloadType();

        /// <summary>
        /// Returns the payload string that defines query on what Entity
        /// result is needed to continue the mapping.
        /// </summary>
        /// <returns>the payload containing info needed to execute the query</returns>
        string GetPayload();

        /// <summary>
        /// Returns a definition that defines how to iterate through each
        /// entity in result.
        /// </summary>
        /// <returns>the value containing the iteration definition</returns>
        string GetIteratorDef();

        /// <summary>
        /// Set the value of a property key for this configuration.
        /// </summary>
        /// <param name="key">the given property name</param>
        /// <param name="value">the value associated with given key</param>
        void SetProperty(string key, string value);

        /// <summary>
        /// Returns the value of a property in this configuration.
        /// </summary>
        /// <param name="key">the given property name to retrieve its value</param>
        /// <returns>the value of the given property otherwise null</returns>
        string GetProperty(string key);

    }
}

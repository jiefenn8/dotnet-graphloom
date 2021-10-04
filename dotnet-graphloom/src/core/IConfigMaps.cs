//-----------------------------------------------------------------------
// <copyright file="IConfigMaps.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using GraphLoom.Mapper.Core.ConfigMaps;

namespace GraphLoom.Mapper.Core
{
    /// <summary>
    /// This interface defines the base methods to express the rules
    /// from many configurations that will map data-source to a graph model.
    /// </summary>
    public interface IConfigMaps : IEnumerable<IEntityMap>
    {
        /// <summary>
        /// Returns map of all namespace prefix and URI that was used in the
        /// mapping document.
        /// </summary>
        /// <returns>the map containing all namespace prefixes and their URIs</returns>
        Dictionary<string, string> GetNamespaceMap();

        /// <summary>
        /// Returns all unique entity map that exists in the configuration
        /// mappings.
        /// </summary>
        /// <returns>the set containing all unique entity maps</returns>
        ISet<IEntityMap> GetEntityMaps();
    }
}

//-----------------------------------------------------------------------
// <copyright file="IGraphMapper.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using GraphLoom.Mapper.Configuration;
using GraphLoom.Mapper.Core;
using VDS.RDF;

namespace GraphLoom.Mapper
{

    /// <summary>
    /// This interface defines the base methods that manages the mapping
    /// of an input source using provided mapping configurations.
    /// </summary>
    public interface IGraphMapper
    {
        [Obsolete("This method is deprecated and will be removed in the future. Please use the new (Graph MapToGraph) method.")]
        IGenericGraph MapToGraph(IInputSource source, IMapperConfig config);

        [Obsolete("This method is deprecated and will be removed in the future.")]
        Type GetGraphType();

        [Obsolete("This method is deprecated and will be removed in the future.")]
        void StopTask();

        /// <summary>
        /// Returns the resulting mapping of input source applied to the config
        /// mappings given.Returns an empty model if the given input source or
        /// configuration mappings was not sufficient to generate any semantic 
        /// terms.
        /// </summary>
        /// <param name="source">the source containing the data to map over to graph</param>
        /// <param name="configMaps">he configs to manage the mapping of data</param>
        /// <returns>the model containing the mapped source as a graph model</returns>
        Graph MapToGraph(IInputSource source, IConfigMaps configMaps);
    }
}

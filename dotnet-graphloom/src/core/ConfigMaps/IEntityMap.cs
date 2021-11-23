//-----------------------------------------------------------------------
// <copyright file="IEntityMap.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using GraphLoom.Mapper.Core.InputSource;
using VDS.RDF;

namespace GraphLoom.Mapper.Core.ConfigMaps
{
    /// <summary>
    /// This interface defines the base methods that manages the mapping
    /// of data to their graph terms sharing the same entity.
    /// </summary>
    public interface IEntityMap
    {
        /// <summary>
        /// Returns the unique id name that identify this instance.
        /// </summary>
        /// <returns>unique id name of this entity map</returns>
        string getIdName();

        /// <summary>
        /// Returns the source result with the source and query prepareded to be 
        /// excututed to acquire the result to iterate through. 
        /// </summary>
        /// <returns>the source result associated with this entity map</returns>
        ISourceResult GetSourceResult();

        /// <summary>
        /// Returns generated entity term of this instance. If the term cannot be
        /// generated, returns null.
        /// </summary>
        /// <param name="entity">containing the entity data to generate the term</param>
        /// <returns>the URI term generated for this entity, otherwise null</returns>
        IUriNode GenerateEntityTerm(IEntity entity);

        /// <summary>
        /// Returns generated entity term of this instance. If the term cannot 
        /// be generated, returns null.
        /// </summary>
        /// <param name="term">containing the entity data to generate the term</param>
        /// <returns>the URI term generated for this entity, otherwise null</returns>
        IGraph GenerateClassTerms(IUriNode term);

        /// <summary>
        /// Returns true if this entity mapping has any relation map and
        /// node map pairs.
        /// </summary>
        /// <returns>true if there are any pair, otherwise false</returns>
        bool HasNodePairs();

        /// <summary>
        /// Returns a model containing RDF triples of all entity properties.
        /// </summary>
        /// <param name="term">of the entity that represent this entity map</param>
        /// <param name="entity">containing the entity data to generate the term</param>
        /// <returns>model containing RDF triples related to an entity</returns>
        IGraph GenerateNodeTerms(IUriNode term, IEntity entity);

        /// <summary>
        /// Returns a model containing RDF triples of all entity properties 
        /// that reference to an existing entity in the <see cref="IConfigMaps"/> 
        /// that this instance belongs to.
        /// </summary>
        /// <param name="term">of the entity that represent this entity map</param>
        /// <param name="source">containing the data source to query data</param>
        /// <returns>model containing RDF triples related to an entity</returns>
        IGraph GenerateRefNodeTerms(IUriNode term, IInputSource source);
    }
}

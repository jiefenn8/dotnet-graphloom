//-----------------------------------------------------------------------
// <copyright file="ISourceResult.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using GraphLoom.Mapper.Core.InputSource;

namespace GraphLoom.Mapper.Core.ConfigMaps
{
    /// <summary>
    /// This interface defines the base methods that provides the information
    /// needed to locate and retrieve the desired data from the data-source.
    /// </summary>
    public interface ISourceResult
    {
        /// <summary>
        /// Returns the <see cref="IEntityReference"/> associated with this 
        /// instance. Every instance should have a reference containing the
        /// required information to locate the specific data from the source
        /// to map into an entity.
        /// </summary>
        /// <returns>the reference containing information to locate entity data</returns>
        IEntityReference GetEntityReference();

        /// <summary>
        /// Iterates through the received collection of entities from source and
        /// apply any defined actions to each entity.
        /// </summary>
        /// <param name="source">containing the data source to query</param>
        /// <param name="action">to apply to each entity found</param>
        void ForEachEntity(IInputSource source, Action<IEntity> action)
        {
            if (action is null) throw new ArgumentNullException();
            source.ExecuteEntityQuery(GetEntityReference(), (r) =>
            {
                while (r.HasNext())
                {
                    action.Invoke(r.NextEntity());
                }
            });
        }
    }
}

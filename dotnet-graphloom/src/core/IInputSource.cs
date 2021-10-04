//-----------------------------------------------------------------------
// <copyright file="IInputSource.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using GraphLoom.Mapper.Core.InputSource;

namespace GraphLoom.Mapper
{
    /// <summary>
    /// This interface defines the base methods in retrieving relevant data 
    /// grouped through a defined entity; usually by name reference or a query.
    /// </summary>
    public interface IInputSource
    {
        [Obsolete("This method is deprecated and will be removed in the future.")]
        List<IDictionary<string, string>> GetEntityRecords(string entity);

        /// <summary>
        /// Execute the reference or query in <see cref="BaseEntityReference"/>
        /// to obtain relevant data as <see cref="IEntityResult"/>and apply any 
        /// action to it.
        /// </summary>
        /// <param name="entityRef"></param>
        /// <param name="action"></param>
        void ExecuteEntityQuery(IEntityReference entityRef, Action<IEntityResult> action);
    }
}

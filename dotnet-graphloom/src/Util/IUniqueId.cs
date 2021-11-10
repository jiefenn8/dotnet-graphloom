//-----------------------------------------------------------------------
// <copyright file="UniqueId.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace GraphLoom.Mapper.Util
{
    /// <summary>
    /// This interface defines the base methods in uniquely identify an
    /// instance of an implementation.
    /// </summary>
    public interface IUniqueId
    {
        /// <summary>
        /// Returns the <see cref="Guid"/> as string generated in this instance.
        /// </summary>
        /// <returns>Guid of this instance</returns>
        string GetUniqueId();
    }
}

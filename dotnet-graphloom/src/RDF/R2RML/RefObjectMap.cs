//-----------------------------------------------------------------------
// <copyright file="RefObjectMap.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using GraphLoom.Mapper.Core.ConfigMaps;
using GraphLoom.Mapper.Core.InputSource;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF.R2RML
{
    /// <summary>
    /// Implementation of R2RML RefObjectMap with <see cref="INodeMap"/> interface.
    /// </summary>
    public class RefObjectMap : INodeMap
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public INode GenerateNodeTerm(IEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TriplesMapFuture GetParentTriplesMap()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ISet<JoinCondition> ListJoinConditions()
        {
            throw new NotImplementedException();
        }
    }
}

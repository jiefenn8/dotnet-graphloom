//-----------------------------------------------------------------------
// <copyright file="JoinCondition.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace GraphLoom.Mapper.RDF.R2RML
{
    /// <summary>
    /// Implementation of R2RML JoinCondition. This class defines
    /// the base methods that manages the joins between two
    /// different queries.
    /// </summary>
    public class JoinCondition
    {
        /// <summary>
        /// Gets the parent identifier of the join. 
        /// </summary>
        private string Parent { get; }

        /// <summary>
        /// Gets the child identifier of the join.
        /// </summary>        
        private string Child { get; }

        /// <summary>
        /// Constructs a JoinCondition with the specified parent
        /// and child columns.
        /// </summary>
        /// <param name="parent">the parent column to join to child</param>
        /// <param name="child">the child column to join to parent</param>
        public JoinCondition(string parent, string child)
        {
            Parent = parent ?? throw new ArgumentNullException();
            Child = child ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Returns the string version of an SQL Join for both child and parent.
        /// </summary>
        /// <returns>string of the joined child and parent</returns>
        public string GetJoinString()
        {
            return "child." + Child + "=parent." + Parent;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Parent, Child);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is JoinCondition condition &&
                   Parent == condition.Parent &&
                   Child == condition.Child;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

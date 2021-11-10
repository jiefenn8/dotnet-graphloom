//-----------------------------------------------------------------------
// <copyright file="ParserException.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace GraphLoom.Mapper.Exceptions
{
    /// <summary>
    /// This class defines extends the <see cref="Exception"/> class and 
    /// handles any exceptions arising from any related parsing of files
    /// for R2RML.
    /// </summary>
    public class ParserException : Exception
    {
        /// <inheritdoc/>
        public ParserException() { }

        /// <inheritdoc/>
        public ParserException(string message) : base(message) { }

        /// <inheritdoc/>
        public ParserException(string message, Exception exception) : base(message, exception) { }
    }
}

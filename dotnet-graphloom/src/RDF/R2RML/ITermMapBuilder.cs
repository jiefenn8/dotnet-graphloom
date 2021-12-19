//-----------------------------------------------------------------------
// <copyright file="ITermMapBuilder.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF.R2RML
{
    /// <summary>
    /// This interface defines the base methods that manages the
    /// configuration and creation of term map instance. 
    /// </summary>
    public interface ITermMapBuilder<T> where T : ITermMap
    {
        /// <summary>
        /// Gets base URI for URI term generation. 
        /// </summary>
        public IUriNode BaseUri { get; }

        /// <summary>
        /// Gets base value for RDF term generation.
        /// </summary>
        public INode BaseValue { get; }

        /// <summary>
        /// Gets valued type of this term map instance.
        /// </summary>
        public ValuedType ValuedType { get; }

        /// <summary>
        /// Gets term type for RDF term generation. Overrides default. 
        /// </summary>
        public TermType TermType { get; }

        /// <summary>
        /// Gets language tag for literal generation.
        /// </summary>
        public string Lang { get; }

        /// <summary>
        /// Gets data type for literal generation.
        /// </summary>
        public Uri DataType { get; }

        /// <summary>
        /// Sets the RDF type the TermMap should return after generating an
        /// RDF term.
        /// </summary>
        /// <param name="termType">the term type to return term output as</param>
        /// <returns>this builder for method chaining</returns>
        public ITermMapBuilder<T> SetTermType(TermType termType);

        /// <summary>
        /// Sets the RDF term language tag for this TermMap.
        /// </summary>
        /// <param name="lang">the term language tag that this term will be generated as</param>
        /// <returns>this builder for method chaining</returns>
        public ITermMapBuilder<T> SetLanguage(string lang);

        /// <summary>
        /// Sets the RDF term data type for this TermMap.
        /// </summary>
        /// <param name="dataType">the data type that this term will be generated as</param>
        /// <returns>this builder for method chaining</returns>
        public ITermMapBuilder<T> SetDataType(Uri dataType);

        /// <summary>
        /// Returns an instance of TermMap with the specified data given to
        /// this builder.
        /// </summary>
        /// <returns>TermMap instance with populated fields from builder</returns>
        public T Build();
    }
}

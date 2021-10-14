//-----------------------------------------------------------------------
// <copyright file="AbstractTermMapBuilder.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using GraphLoom.Mapper.RDF.R2RML;
using VDS.RDF;

namespace GraphLoom.Mapper.src.RDF.R2RML
{
    /// <summary>
    /// ENUM to manage different valued TermMaps.
    /// </summary>
    public enum ValuedType
    {
        /// <summary>
        /// If the base value if constant.
        /// </summary>
        CONSTANT,

        /// <summary>
        /// If the base value is a template.
        /// </summary>
        TEMPLATE,

        /// <summary>
        /// If the base value is a column reference.
        /// </summary>
        COLUMN
    }

    /// <summary>
    /// This abstract class defines the implementation of the base
    /// methods specified  in the <see cref="ITermMapBuilder"/>
    /// interface.
    /// <para/>
    /// It is recommended to defined your extended functionality to 
    /// this class and then call them before calling the abstract 
    /// methods below; Except for overrides.
    /// </summary>
    public abstract class AbstractTermMapBuilder : ITermMapBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractTermMapBuilder"/> class with 
        /// a specified base value and TermMap valued type.
        /// </summary>
        /// <param name="baseUri">to be used to generate RDF URI term for this TermMap</param>
        /// <param name="baseValue">to be used to generate RDF term for this TermMap</param>
        /// <param name="valuedType">to identify the TermMap valued type, this will
        /// determine how the base value will be used
        /// </param>
        public AbstractTermMapBuilder(IUriNode baseUri, INode baseValue, ValuedType valuedType)
        {
            BaseUri = baseUri;
            BaseValue = baseValue;
            ValuedType = valuedType;
        }

        /// <inheritdoc/>
        public IUriNode BaseUri { get; private set; }

        /// <inheritdoc/>
        public INode BaseValue { get; private set; }

        /// <inheritdoc/>
        public ValuedType ValuedType { get; private set; }

        /// <inheritdoc/>
        public TermType TermType { get; private set; } = TermType.UNDEFINED;

        /// <inheritdoc/>
        public string Lang { get; private set; } = string.Empty;

        /// <inheritdoc/>
        public Uri DataType { get; private set; }

        /// <inheritdoc/>
        public ITermMapBuilder SetTermType(TermType termType)
        {
            TermType = termType;
            return this;
        }

        /// <inheritdoc/>
        public ITermMapBuilder SetLanguage(string lang)
        {
            Lang = lang;
            return this;
        }

        /// <inheritdoc/>
        public ITermMapBuilder SetDataType(Uri dataType)
        {
            DataType = dataType;
            return this;
        }

        /// <inheritdoc/>
        public abstract ITermMap Build();
    }
}

//-----------------------------------------------------------------------
// <copyright file="AbstractTermMap.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Text;
using System.Text.RegularExpressions;
using GraphLoom.Mapper.Core.InputSource;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF.R2RML
{
    /// <summary>
    /// This abstract class defines the implementation of the base methods 
    /// specified in the <see cref="TermMap"/> interface.
    /// </summary>
    public abstract class AbstractTermMap : ITermMap
    {
        /// <summary>
        /// Regex to match values within a curly bracket in template. 
        /// </summary>
        private static readonly Regex TemplateRegex = new Regex("{(.*?)}", RegexOptions.Compiled);

        /// <summary>
        /// Node factory to create RDF related terms. 
        /// </summary>
        private readonly INodeFactory factory;

        /// <summary>
        /// Gets base URI for URI term generation. 
        /// </summary>
        private readonly IUriNode baseUri;

        /// <summary>
        /// Gets base value for RDF term generation.
        /// </summary>
        private readonly INode baseValue;

        /// <summary>
        /// Gets valued type of this term map instance.
        /// </summary>
        private readonly ValuedType valuedType;

        /// <summary>
        /// Gets term type for RDF term generation. Overrides default. 
        /// </summary>
        private readonly TermType termType;

        /// <summary>
        /// Gets language tag for literal generation.
        /// </summary>
        private readonly string lang;

        /// <summary>
        /// Gets data type for literal generation.
        /// </summary>
        private readonly Uri dataType;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractTermMap"/> class
        /// with the specified TermMap Builder.
        /// </summary>
        /// <param name="builder">the TermMap Builder to build instance from</param>
        public AbstractTermMap(ITermMapBuilder builder)
        {
            factory = new NodeFactory();
            baseUri = builder.BaseUri;
            baseValue = builder.BaseValue;
            valuedType = builder.ValuedType;
            termType = builder.TermType;
            lang = builder.Lang;
            dataType = builder.DataType;
        }

        /// <inheritdoc/>
        public INode GenerateRDFTerm(IEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Entity is null.");
            }

            INode term;
            switch (valuedType)
            {
                case ValuedType.CONSTANT:
                    term = CreateConstantTerm();
                    break;
                case ValuedType.TEMPLATE:
                    term = CreateTemplateTerm(entity);
                    break;
                case ValuedType.COLUMN:
                    term = CreateColumnTerm(entity);
                    break;
                default:
                    throw new ArgumentException("Term map valued type is neither CONSTANT, TEMPLATE or COLUMN.");
            }

            return term;
        }

        /// <summary>
        /// Returns the term created from the given value and term type
        /// specified to be mapped to.
        /// </summary>
        /// <param name="value">the String value of the term to turn into RDF</param>
        /// <param name="type">the term type to map the value into</param>
        /// <returns>the generated term value to the type specified</returns>
        protected INode AsRDFTerm(string value, TermType type)
        {
            INode term;
            if (value is null)
            {
                throw new ArgumentNullException();
            }

            switch (type)
            {
                case TermType.IRI:
                    term = HandleUriGeneration(value);
                    break;
                case TermType.BLANK:
                    term = factory.CreateBlankNode();
                    break;
                case TermType.LITERAL:
                    term = GenerateSpecificLiteral(value);
                    break;
                default:
                    term = HandleDefaultGeneration(value);
                    break;
            }

            return term;
        }

        /// <summary>
        /// Returns the term created from the given value with the default
        /// generation rule defined.
        /// </summary>
        /// <param name="term">the string value of the term to turn into RDF</param>
        /// <returns>the generated term value with the default generation defined</returns>
        protected abstract INode HandleDefaultGeneration(string term);

        /// <summary>
        /// Returns the base value as a constant RDF term. Constant term 
        /// does not require any further interaction with any source to 
        /// generate RDF term.
        /// </summary>
        /// <returns>the constant value to use as RDF term</returns>
        private INode CreateConstantTerm()
        {
            return baseValue;
        }

        /// <summary>
        /// Returns a generated RDF term using the base value as template 
        /// with the value from entity source.The base value (as template) 
        /// MUST specify a valid column name identifier that can be used 
        /// with entity to retrieve the target data.Column name syntax 
        /// rules as follows:
        /// <para/>
        /// - At least one column name in template.
        /// - Column names are enclosed by curly braces '{column_name}'.
        /// - Any curly braces within the column_name must be escaped.
        /// - Multiple column names should be separate from each other.
        /// </summary>
        /// <param name="entity">the entity source containing the data for generation</param>
        /// <returns>the term generated, otherwise null if no value cannot be found</returns>
        private INode CreateTemplateTerm(IEntity entity)
        {
            string template = baseValue.ToString();
            Match matcher = TemplateRegex.Match(template);
            if (matcher.Success is false)
            {
                throw new ArgumentException("Template given cannot be matched. Must have: {name}.");
            }

            string value = entity.GetPropertyValue(matcher.Groups[1].Value);
            if (!string.IsNullOrEmpty(value))
            {
                byte[] encodedBytes = Encoding.UTF8.GetBytes(value);
                value = Encoding.UTF8.GetString(encodedBytes);
                string term = template.Replace(matcher.Groups[0].Value, value);
                return AsRDFTerm(term, termType);
            }

            return null;
        }

        /// <summary>
        /// Returns a generated RDF term using the base value as the column 
        /// name identifier to retrieve the value from entity source.
        /// </summary>
        /// <param name="entity">the entity source containing the data for generation</param>
        /// <returns>the value retrieved from source as RDF term</returns>
        private INode CreateColumnTerm(IEntity entity)
        {
            string column = baseValue.ToString();
            string value = entity.GetPropertyValue(column);
            return value == null ? null : AsRDFTerm(value, termType);
        }

        /// <summary>
        /// Returns an URI term created from the given value. If creating
        /// an uri using the given value is not possible, return a generated 
        /// uri using both the value and _baseUri.
        /// </summary>
        /// <param name="value">the String value of the term to turn into RDF</param>
        /// <returns>the generated term as an valid URI</returns>
        private IUriNode HandleUriGeneration(string value)
        {
            try
            {
                return factory.CreateUriNode(UriFactory.Create(value));
            }
            catch (UriFormatException)
            {
                return GenerateTermFromBaseUri(value);
            }
        }

        /// <summary>
        /// Returns an URI term created from the given value and base uri.
        /// Throws an ArgumentException if the URI term cannot be generated 
        /// from the value and _baseUri.
        /// </summary>
        /// <param name="value">the string value of the term to turn into RDF</param>
        /// <returns>the generated term as an valid URI, else throw an exception</returns>
        private IUriNode GenerateTermFromBaseUri(string value)
        {
            try
            {
                string uri = baseUri.Uri.OriginalString;
                Uri newUri = UriFactory.Create(uri + value);
                return factory.CreateUriNode(newUri);
            }
            catch (NullReferenceException ex)
            {
                throw new ArgumentException("Failed to generate new term with base uri and value.", ex);
            }
        }

        /// <summary>
        /// Returns a specific literal term created from the given value. 
        /// The literal term can be either a standard literal, typed 
        /// literal or a language tagged literal depending if lang or 
        /// _data type has been defined in this instance. 
        /// </summary>
        /// <param name="term">the string value of the term to turn into RDF</param>
        /// <returns>the generated term value to the literal type specified</returns>
        private ILiteralNode GenerateSpecificLiteral(string term)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                return factory.CreateLiteralNode(term, lang);
            }

            if (dataType != null)
            {
                return factory.CreateLiteralNode(term, dataType);
            }

            return factory.CreateLiteralNode(term);
        }
    }
}

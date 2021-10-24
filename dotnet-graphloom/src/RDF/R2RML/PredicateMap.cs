//-----------------------------------------------------------------------
// <copyright file="PredicateMap.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using GraphLoom.Mapper.Core.ConfigMaps;
using GraphLoom.Mapper.Core.InputSource;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF.R2RML
{
    /// <summary>
    /// Implementation of R2RML PredicateMap with <see cref="IRelationMap"/> 
    /// interface. This term map will return either a rr:IRI for its main term.
    /// </summary>
    public class PredicateMap : AbstractTermMap<PredicateMap>, IRelationMap
    {
        /// <summary>
        /// Constructs an PredicateMap with the specified TermMap Builder 
        /// containing the data to initialise an immutable instance.
        /// </summary>
        /// <param name="builder">the TermMap Builder to builder instance from</param>
        private PredicateMap(Builder builder) : base(builder) { }

        /// <inheritdoc/>
        public IUriNode GenerateRelationTerm(IEntity entity)
        {
            INode term = GenerateRDFTerm(entity);
            return term == null ? null : (IUriNode)term;
        }

        /// <inheritdoc/>
        protected override INode HandleDefaultGeneration(string term)
        {
            return AsRDFTerm(term, TermType.IRI);
        }

        /// <summary>
        /// Builder class for <see cref="PredicateMap"/>.
        /// </summary>
        public class Builder : AbstractTermMapBuilder<PredicateMap>
        {
            /// <inheritdoc/>
            public Builder(IUriNode baseUri, INode baseValue, ValuedType valuedType) : base(baseUri, baseValue, valuedType) { }

            /// <inheritdoc/>
            public override PredicateMap Build()
            {
                return new PredicateMap(this);
            }
        }
    }
}

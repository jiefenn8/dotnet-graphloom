//-----------------------------------------------------------------------
// <copyright file="ObjectMapFuture.cs" company="github.com/jiefenn8">
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
    /// Implementation of R2RML ObjectMap with <see cref="INodeMap"/> 
    /// interface. This term map will return either a rr:IRI, 
    /// rr:BlankNode or rr:Literal for its main term.
    /// </summary>
    public class ObjectMapFuture : AbstractTermMap<ObjectMapFuture>, INodeMap
    {
        /// <inheritdoc/>
        private ObjectMapFuture(Builder builder) : base(builder) { }

        /// <inheritdoc/>
        public INode GenerateNodeTerm(IEntity entity)
        {
            return GenerateRDFTerm(entity);
        }

        /// <inheritdoc/>
        protected override INode HandleDefaultGeneration(string term)
        {
            if (IsColumnValuedType() || IsSpecialLiteral())
            {
                return AsRDFTerm(term, TermType.LITERAL);
            }
            return AsRDFTerm(term, TermType.IRI);
        }

        /// <summary>
        /// Builder class for <see cref="ObjectMapFuture"/>
        /// </summary>
        public class Builder : AbstractTermMapBuilder<ObjectMapFuture>
        {
            /// <inheritdoc/>
            public Builder(IUriNode baseUri, INode baseValue, ValuedType valuedType) : base(baseUri, baseValue, valuedType) { }

            /// <inheritdoc/>
            public override ObjectMapFuture Build()
            {
                return new ObjectMapFuture(this);
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="MockTermMap.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using GraphLoom.Mapper.RDF.R2RML;
using VDS.RDF;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    /// <summary>
    /// Mock class for <see cref="ITermMap"/> interface.
    /// </summary>
    public class MockTermMap : AbstractTermMap<MockTermMap>
    {
        public MockTermMap(Builder builder) : base(builder) { }

        protected override INode HandleDefaultGeneration(string term)
        {
            return AsRDFTerm(term, TermType.IRI);
        }

        /// <summary>
        /// Mock class for <see cref="ITermMapBuilder"/> interface.
        /// </summary>
        public class Builder : AbstractTermMapBuilder<MockTermMap>
        {
            public Builder(IUriNode baseUri, INode baseValue, ValuedType valuedType) : base(baseUri, baseValue, valuedType) { }

            public override MockTermMap Build()
            {
                return new MockTermMap(this);
            }
        }
    }
}

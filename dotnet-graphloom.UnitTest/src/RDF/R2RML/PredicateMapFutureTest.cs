//-----------------------------------------------------------------------
// <copyright file="PredicateMapTest.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using GraphLoom.Mapper.Core.InputSource;
using GraphLoom.Mapper.RDF.R2RML;
using Moq;
using NUnit.Framework;
using VDS.RDF;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    /// <summary>
    /// Unit test class for <see cref="PredicateMapFuture"/> and its iBuilder.
    /// </summary>
    public class PredicateMapFutureTest
    {
        private NodeFactory nodeFactory;
        private IUriNode baseUri;
        private IEntity mockEntity;
        private PredicateMapFuture predicateMap;

        [SetUp]
        public void SetUp()
        {
            nodeFactory = new NodeFactory();
            mockEntity = Mock.Of<IEntity>();
            baseUri = nodeFactory.CreateUriNode(UriFactory.Create("http://example.com/"));
        }

        public static IEnumerable<TestCaseData> TermMapArguments()
        {
            NodeFactory nodeFactory = new NodeFactory();
            yield return new TestCaseData(
                ValuedType.TEMPLATE,
                nodeFactory.CreateLiteralNode("http://data.example.com/{REFERENCE}"),
                nodeFactory.CreateUriNode(UriFactory.Create("http://data.example.com/VALUE"))
                );
            yield return new TestCaseData(
                ValuedType.COLUMN,
                nodeFactory.CreateLiteralNode("REFERENCE"),
                nodeFactory.CreateUriNode(UriFactory.Create("http://data.example.com/VALUE"))
                );
        }

        [Test, TestCaseSource(nameof(TermMapArguments))]
        public void Generate_term_with_no_type(ValuedType valuedType, INode baseValue, INode expected)
        {
            Mock.Get(mockEntity).Setup(f => f.GetPropertyValue(It.Is<string>(i => i.Equals("REFERENCE"))))
                .Returns("http://data.example.com/VALUE");
            SubjectMapFuture.Builder builder = new SubjectMapFuture.Builder(baseUri, baseValue, valuedType);

        }

        public static IEnumerable<TestCaseData> ValuedTermArguments()
        {
            yield return new TestCaseData(ValuedType.CONSTANT);
            yield return new TestCaseData(ValuedType.COLUMN);
            yield return new TestCaseData(ValuedType.TEMPLATE);
        }

        [Test, TestCaseSource(nameof(ValuedTermArguments))]
        public void Generate_term_with_null_entity_is_not_possible(ValuedType valuedType)
        {
            PredicateMapFuture.Builder builder = new PredicateMapFuture.Builder(baseUri, Mock.Of<INode>(), valuedType);
            predicateMap = builder.Build();
            Assert.Throws<ArgumentNullException>(
                () => predicateMap.GenerateRelationTerm(null),
                "Entity is null."
                );
        }
    }
}

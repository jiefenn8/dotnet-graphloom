//-----------------------------------------------------------------------
// <copyright file="ObjectMapTest.cs" company="github.com/jiefenn8">
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
    /// Unit test class for <see cref="ObjectMapFuture"/> and its Builder.
    /// </summary>
    public class ObjectMapFutureTest
    {
        private NodeFactory nodeFactory;
        private IUriNode baseUri;
        private IEntity mockEntity;
        private ObjectMapFuture objectMap;

        [SetUp]
        public void SetUp()
        {
            nodeFactory = new NodeFactory();
            mockEntity = Mock.Of<IEntity>();
            baseUri = nodeFactory.CreateUriNode(UriFactory.Create("http://example.com"));
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
                nodeFactory.CreateLiteralNode("VALUE")
                );
        }

        [Test, TestCaseSource(nameof(TermMapArguments))]
        public void Generate_term_with_no_type(ValuedType valuedType, INode baseValue, INode expected)
        {
            Mock.Get(mockEntity).Setup(f => f.GetPropertyValue(It.Is<string>(i => i.Equals("REFERENCE"))))
                .Returns("VALUE");
            objectMap = new ObjectMapFuture.Builder(baseUri, baseValue, valuedType).Build();
            INode result = objectMap.GenerateNodeTerm(mockEntity);
            Assert.That(result, Is.EqualTo(expected));
        }

        public static IEnumerable<TestCaseData> TermMapArgumentsWithDataType()
        {
            NodeFactory nodeFactory = new NodeFactory();
            Uri dataType = UriFactory.Create("https://www.w3.org/2001/XMLSchema#positiveInteger");
            yield return new TestCaseData(
                ValuedType.TEMPLATE,
                nodeFactory.CreateLiteralNode("http://data.example.com/{REFERENCE}"),
                dataType,
                nodeFactory.CreateLiteralNode("http://data.example.com/1234", dataType)
                );
            yield return new TestCaseData(
                ValuedType.COLUMN,
                nodeFactory.CreateLiteralNode("REFERENCE"),
                dataType,
                nodeFactory.CreateLiteralNode("1234", dataType)
                );
        }

        [Test, TestCaseSource(nameof(TermMapArgumentsWithDataType))]
        public void Generate_literal_with_datatype(ValuedType valuedType, INode baseValue, Uri dataType, INode expected)
        {
            Mock.Get(mockEntity).Setup(f => f.GetPropertyValue(It.Is<string>(i => i.Equals("REFERENCE"))))
                .Returns("1234");
            ObjectMapFuture.Builder builder = new ObjectMapFuture.Builder(baseUri, baseValue, valuedType);
            objectMap = builder.SetDataType(dataType).Build();
            INode result = objectMap.GenerateNodeTerm(mockEntity);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}

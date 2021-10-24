//-----------------------------------------------------------------------
// <copyright file="AbstractTermMapTest.cs" company="github.com/jiefenn8">
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
    /// Unit test class for <see cref="AbstractTermMap"/>.
    /// </summary>
    [TestFixture(typeof(SubjectMap))]
    [TestFixture(typeof(PredicateMap))]
    public class AbstractTermMapTest<T> where T : ITermMap
    {
        private NodeFactory nodeFactory;
        private IUriNode baseUri;
        private MockTermMap termMap;
        private IEntity mockEntity;
        private INode mockNode;

        [SetUp]
        public void SetUp()
        {
            nodeFactory = new NodeFactory();
            baseUri = nodeFactory.CreateUriNode(UriFactory.Create("http://example.com/"));
            mockEntity = Mock.Of<IEntity>();
            mockNode = Mock.Of<IUriNode>();
            Mock.Get(mockEntity).Setup(f => f.GetPropertyValue(It.Is<string>(s => s == "REFERENCE"))).Returns("VALUE");
        }

        private static IEnumerable<TestCaseData> valuedTermArguments()
        {
            yield return new TestCaseData(ValuedType.COLUMN);
            yield return new TestCaseData(ValuedType.CONSTANT);
            yield return new TestCaseData(ValuedType.TEMPLATE);
        }

        [Test, TestCaseSource(nameof(valuedTermArguments))]
        public void Generate_term_without_entity_is_not_possble(ValuedType valuedType)
        {
            MockTermMap.Builder builder = new MockTermMap.Builder(baseUri, mockNode, valuedType);
            termMap = builder.Build();
            Assert.Throws<ArgumentNullException>(
                () => termMap.GenerateRDFTerm(null),
                "Entity is null."
                );
        }

        private static IEnumerable<TestCaseData> termMapConstantArguments()
        {
            INodeFactory nodeFactory = new NodeFactory();
            IUriNode uriNode = nodeFactory.CreateUriNode(UriFactory.Create("http://example.com/SUBJECT"));
            INode literalNode = nodeFactory.CreateLiteralNode("VALUE");
            IBlankNode blankNode = nodeFactory.CreateBlankNode();
            yield return new TestCaseData(uriNode, TermType.IRI, uriNode);
            yield return new TestCaseData(uriNode, TermType.LITERAL, uriNode);
            yield return new TestCaseData(uriNode, TermType.BLANK, uriNode);
            yield return new TestCaseData(literalNode, TermType.IRI, literalNode);
            yield return new TestCaseData(literalNode, TermType.LITERAL, literalNode);
            yield return new TestCaseData(literalNode, TermType.BLANK, literalNode);
            yield return new TestCaseData(blankNode, TermType.IRI, blankNode);
            yield return new TestCaseData(blankNode, TermType.LITERAL, blankNode);
            yield return new TestCaseData(blankNode, TermType.BLANK, blankNode);
        }

        [Test, TestCaseSource(nameof(termMapConstantArguments))]
        public void Constant_term_map_ignores_specified_type(INode baseValue, TermType termType, INode expected)
        {
            MockTermMap.Builder builder = new MockTermMap.Builder(baseUri, baseValue, ValuedType.CONSTANT);
            termMap = builder.SetTermType(termType).Build();
            INode result = termMap.GenerateRDFTerm(mockEntity);
            Assert.That(result, Is.EqualTo(expected));
        }

        private static IEnumerable<TestCaseData> termMapArguments()
        {
            INodeFactory factory = new NodeFactory();
            yield return new TestCaseData(ValuedType.TEMPLATE, factory.CreateLiteralNode("http://example.com/{REFERENCE}"), factory.CreateUriNode(new Uri("http://example.com/VALUE")));
            yield return new TestCaseData(ValuedType.COLUMN, factory.CreateLiteralNode("REFERENCE"), factory.CreateUriNode(UriFactory.Create("http://example.com/VALUE")));
        }

        [Test, TestCaseSource(nameof(termMapArguments))]
        public void Generate_term_with_entity(ValuedType valuedType, INode baseValue, INode expected)
        {
            MockTermMap.Builder builder = new MockTermMap.Builder(baseUri, baseValue, valuedType);
            termMap = builder.Build();
            INode result = termMap.GenerateRDFTerm(mockEntity);
            Assert.That(result, Is.EqualTo(expected));
        }

        private static IEnumerable<TestCaseData> termMapArgumentsWithBlankType()
        {
            INodeFactory factory = new NodeFactory();
            yield return new TestCaseData(ValuedType.TEMPLATE, factory.CreateLiteralNode("http://example.com/{REFERENCE}"));
            yield return new TestCaseData(ValuedType.COLUMN, factory.CreateLiteralNode("REFERENCE"));
        }

        [Test, TestCaseSource(nameof(termMapArgumentsWithBlankType))]
        public void Generate_term_with_blank_type_should_return_blank_node(ValuedType valuedType, INode baseValue)
        {
            MockTermMap.Builder builder = new MockTermMap.Builder(baseUri, baseValue, valuedType);
            termMap = builder.SetTermType(TermType.BLANK).Build();
            NodeType result = termMap.GenerateRDFTerm(mockEntity).NodeType;
            Assert.That(result, Is.EqualTo(NodeType.Blank));
        }

        private static IEnumerable<TestCaseData> termMapArgumentsWithNonBlankType()
        {
            INodeFactory factory = new NodeFactory();
            yield return new TestCaseData(ValuedType.TEMPLATE, factory.CreateLiteralNode("http://example.com/{REFERENCE}"), TermType.IRI, factory.CreateUriNode(UriFactory.Create("http://example.com/VALUE")));
            yield return new TestCaseData(ValuedType.TEMPLATE, factory.CreateLiteralNode("http://example.com/{REFERENCE}"), TermType.LITERAL, factory.CreateLiteralNode("http://example.com/VALUE"));
            yield return new TestCaseData(ValuedType.COLUMN, factory.CreateLiteralNode("REFERENCE"), TermType.IRI, factory.CreateUriNode(UriFactory.Create("http://example.com/VALUE")));
            yield return new TestCaseData(ValuedType.COLUMN, factory.CreateLiteralNode("REFERENCE"), TermType.LITERAL, factory.CreateLiteralNode("VALUE"));
        }

        [Test, TestCaseSource(nameof(termMapArgumentsWithNonBlankType))]
        public void Generate_term_using_non_blank_type(ValuedType valuedType, INode baseValue, TermType type, INode expected)
        {
            MockTermMap.Builder builder = new MockTermMap.Builder(baseUri, baseValue, valuedType);
            termMap = builder.SetTermType(type).Build();
            INode result = termMap.GenerateRDFTerm(mockEntity);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Generate_column_valued_IRI_without_base_URI_is_not_possible()
        {
            INode value = nodeFactory.CreateLiteralNode("REFERENCE");
            MockTermMap.Builder builder = new MockTermMap.Builder(null, value, ValuedType.COLUMN);
            termMap = builder.SetTermType(TermType.IRI).Build();
            Assert.Throws<ArgumentException>(
                () => termMap.GenerateRDFTerm(mockEntity),
                "Failed to generate new term with base uri and value."
                );
        }
    }
}

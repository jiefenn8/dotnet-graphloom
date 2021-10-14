//-----------------------------------------------------------------------
// <copyright file="AbstractTermMapBuilderTest.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using GraphLoom.Mapper.RDF.R2RML;
using GraphLoom.Mapper.src.RDF.R2RML;
using Moq;
using NUnit.Framework;
using VDS.RDF;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    /// <summary>
    /// Unit test class for <see cref="AbstractTermMapBuilder"/>.
    /// </summary>
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class AbstractTermMapBuilderTest
    {
        private NodeFactory nodeFactory;
        private IUriNode baseUri;
        private ITermMapBuilder builder; 

        [SetUp]
        public void SetUp()
        {
            nodeFactory = new NodeFactory();
            baseUri = nodeFactory.CreateUriNode(UriFactory.Create("http://example.com/"));
        }

        private static IEnumerable<TestCaseData> termMapArguments()
        {
            INodeFactory nodeFactory = new NodeFactory();
            IUriNode uriNode = nodeFactory.CreateUriNode(UriFactory.Create("http://example.com/SUBJECT"));
            INode literalNode = nodeFactory.CreateLiteralNode("VALUE");
            IBlankNode blankNode = nodeFactory.CreateBlankNode();
            yield return new TestCaseData(uriNode, ValuedType.CONSTANT);
            yield return new TestCaseData(uriNode, ValuedType.COLUMN);
            yield return new TestCaseData(uriNode, ValuedType.TEMPLATE);
            yield return new TestCaseData(literalNode, ValuedType.CONSTANT);
            yield return new TestCaseData(literalNode, ValuedType.COLUMN);
            yield return new TestCaseData(literalNode, ValuedType.TEMPLATE);
            yield return new TestCaseData(blankNode, ValuedType.CONSTANT);
            yield return new TestCaseData(blankNode, ValuedType.COLUMN);
            yield return new TestCaseData(blankNode, ValuedType.TEMPLATE);
        }

        [Test, TestCaseSource(nameof(termMapArguments))]
        public void Create_instance_should_return_expected_base_value(INode baseValue, ValuedType valuedType) 
        {
            builder = new MockTermMap.Builder(baseUri, baseValue, valuedType);
            INode result = builder.BaseValue;
            Assert.That(result, Is.EqualTo(baseValue));
        }

        [Test, TestCaseSource(nameof(termMapArguments))]
        public void Create_instance_should_return_expected_base_uri(INode baseValue, ValuedType valuedType)
        {
            builder = new MockTermMap.Builder(baseUri, baseValue, valuedType);
            INode result = builder.BaseUri;
            Assert.That(result, Is.EqualTo(baseUri));
        }

        [Test, TestCaseSource(nameof(termMapArguments))]
        public void Create_instance_should_return_expected_valued_type(INode baseValue, ValuedType valuedType)
        {
            builder = new MockTermMap.Builder(baseUri, baseValue, valuedType);
            ValuedType result = builder.ValuedType;
            Assert.That(result, Is.EqualTo(valuedType));
        }
        private static IEnumerable<TestCaseData> termTypeArguments()
        {
            yield return new TestCaseData(TermType.BLANK, ValuedType.CONSTANT);
            yield return new TestCaseData(TermType.BLANK, ValuedType.COLUMN);
            yield return new TestCaseData(TermType.BLANK, ValuedType.TEMPLATE);
            yield return new TestCaseData(TermType.IRI, ValuedType.CONSTANT);
            yield return new TestCaseData(TermType.IRI, ValuedType.COLUMN);
            yield return new TestCaseData(TermType.IRI, ValuedType.TEMPLATE);
            yield return new TestCaseData(TermType.LITERAL, ValuedType.CONSTANT);
            yield return new TestCaseData(TermType.LITERAL, ValuedType.COLUMN);
            yield return new TestCaseData(TermType.LITERAL, ValuedType.TEMPLATE);
            yield return new TestCaseData(TermType.UNDEFINED, ValuedType.CONSTANT);
            yield return new TestCaseData(TermType.UNDEFINED, ValuedType.COLUMN);
            yield return new TestCaseData(TermType.UNDEFINED, ValuedType.TEMPLATE);
        }

        [NonParallelizable]
        [Test, TestCaseSource(nameof(termTypeArguments))]
        public void Create_instance_term_type_should_return_expected(TermType termType, ValuedType valuedType)
        {
            INode mockNode = Mock.Of<INode>();
            builder = new MockTermMap.Builder(baseUri, mockNode, valuedType);
            builder.SetTermType(termType);
            TermType result = builder.TermType;
            Assert.That(result, Is.EqualTo(termType));
        }

        private static IEnumerable<TestCaseData> valuedTermArguments()
        {
            yield return new TestCaseData(ValuedType.COLUMN);
            yield return new TestCaseData(ValuedType.CONSTANT);
            yield return new TestCaseData(ValuedType.TEMPLATE);
        }

        [NonParallelizable]
        [Test, TestCaseSource(nameof(valuedTermArguments))]
        public void Create_instance_language_should_return_expected(ValuedType valuedType)
        {
            string value = "LANG_STR";
            INode mockNode = Mock.Of<INode>();
            builder = new MockTermMap.Builder(baseUri, mockNode, valuedType);
            builder.SetLanguage(value);
            string result = builder.Lang;
            Assert.That(result, Is.EqualTo(value));
        }

        [Test, TestCaseSource(nameof(valuedTermArguments))]
        public void Create_instance_data_type_should_return_expected(ValuedType valuedType)
        {
            Uri dataType = UriFactory.Create("https://www.w3.org/TR/xmlschema-2/#string");
            INode mockNode = Mock.Of<INode>();
            builder = new MockTermMap.Builder(baseUri, mockNode, valuedType);
            builder.SetDataType(dataType);
            Uri result = builder.DataType;
            Assert.That(result, Is.EqualTo(dataType));
        }

        [Test, TestCaseSource(nameof(valuedTermArguments))]
        public void Create_instance_should_be_non_null(ValuedType valuedType)
        {
            INode mockNode = Mock.Of<INode>();
            builder = new MockTermMap.Builder(baseUri, mockNode, valuedType);
            ITermMap result = builder.Build();
            Assert.That(result, Is.Not.Null);
        }
    }
}

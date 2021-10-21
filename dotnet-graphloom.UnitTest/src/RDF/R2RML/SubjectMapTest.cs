//-----------------------------------------------------------------------
// <copyright file="SubjectMapTest.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using GraphLoom.Mapper.Core.InputSource;
using GraphLoom.Mapper.RDF.R2RML;
using Moq;
using NUnit.Framework;
using VDS.RDF;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    /// <summary>
    /// Unit test class for <see cref="SubjectMap"/> and its iBuilder.
    /// </summary>
    public class SubjectMapTest
    {
        private NodeFactory nodeFactory;
        private IUriNode baseUri;
        private IEntity mockEntity;
        private SubjectMap subjectMap;
        
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
            SubjectMap.Builder builder = new SubjectMap.Builder(baseUri, baseValue, valuedType);

        }

        public static IEnumerable<TestCaseData> ValuedTermArguments()
        {
            yield return new TestCaseData(ValuedType.CONSTANT);
            yield return new TestCaseData(ValuedType.COLUMN);
            yield return new TestCaseData(ValuedType.TEMPLATE);
        }

        [Test, TestCaseSource(nameof(ValuedTermArguments))]
        public void Add_null_class_is_not_possible(ValuedType valuedType)
        {
            SubjectMap.Builder builder = new SubjectMap.Builder(baseUri, Mock.Of<INode>(), valuedType);
            Assert.Throws<ArgumentNullException>(() => builder.AddEntityClasses(null));
        }

        [NonParallelizable]
        [Test, TestCaseSource(nameof(ValuedTermArguments))]
        public void When_add_class_should_return_expected_collection(ValuedType valuedType)
        {
            IUriNode node = nodeFactory.CreateUriNode(UriFactory.Create("http://data.example.com/test"));
            ISet<IUriNode> collection = new HashSet<IUriNode> { node };
            SubjectMap.Builder builder = new SubjectMap.Builder(baseUri, Mock.Of<INode>(), valuedType);
            subjectMap = builder.AddEntityClasses(collection).Build();
            bool result = subjectMap.ListEntityClasses().Contains(node);
            Assert.IsTrue(result);
        }

        [Test, TestCaseSource(nameof(ValuedTermArguments))]
        public void When_no_class_given_should_return_empty_collection(ValuedType valuedType)
        {
            SubjectMap.Builder builder = new SubjectMap.Builder(baseUri, Mock.Of<INode>(), valuedType);
            subjectMap = builder.Build();
            bool result = subjectMap.ListEntityClasses().Any();
            Assert.IsFalse(result);
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="TriplesMapFutureTest.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using GraphLoom.Mapper.Core.ConfigMaps;
using GraphLoom.Mapper.Core.InputSource;
using GraphLoom.Mapper.RDF.R2RML;
using Moq;
using NUnit.Framework;
using VDS.RDF;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    /// <summary>
    /// Unit test class for <see cref="TriplesMapFuture"/> and its builder.
    /// </summary>
    [TestFixture]
    public class TriplesMapFutureTest
    {
        private INodeFactory factory = new NodeFactory();
        private TriplesMapFuture triplesMap;
        private static string IdName = "TEST_ID";
        private static LogicalTable MockLogicalTable = new LogicalTable.Builder(Mock.Of<IEntityReference>()).Build();
        private static IPropertyMap MockPropertyMap = Mock.Of<IPropertyMap>();

        [SetUp]
        public void SetUp()
        {
            triplesMap = new TriplesMapFuture.Builder("TEST_ID", MockLogicalTable, MockPropertyMap).Build();
        }

        [Test]
        public void Return_expected_source_result()
        {
            ISourceResult result = triplesMap.GetSourceResult();
            Assert.That(result, Is.EqualTo(MockLogicalTable));
        }

        [Test]
        public void Return_null_when_entity_value_is_null()
        {
            Mock.Get(MockPropertyMap).Setup(f => f.GenerateEntityTerm(It.IsAny<IEntity>())).Returns((IUriNode)null);
            IUriNode result = triplesMap.GenerateEntityTerm(Mock.Of<IEntity>());
            Assert.That(result, Is.Null);
        }

        public static IEnumerable<TestCaseData> PredicateObjectMapArguments()
        {
            KeyValuePair<IRelationMap, INodeMap> mockKeyPair = new KeyValuePair<IRelationMap, INodeMap>(Mock.Of<IRelationMap>(), Mock.Of<INodeMap>());
            yield return new TestCaseData(new TriplesMapFuture.Builder(IdName, MockLogicalTable, MockPropertyMap).AddPredicateObjectMap(mockKeyPair), true);
            yield return new TestCaseData(new TriplesMapFuture.Builder(IdName, MockLogicalTable, MockPropertyMap), false);
        }

        [Test, TestCaseSource(nameof(PredicateObjectMapArguments))]
        public void Return_expected_evaluation_when_there_is_predicate_object_map(TriplesMapFuture.Builder builder, bool expected)
        {
            TriplesMapFuture triplesMap = builder.Build();
            bool result = triplesMap.HasNodePairs();
            Assert.That(result, Is.EqualTo(expected));
        }

        public static IEnumerable<TestCaseData> EntityClassArguments()
        {
            INodeFactory factory = new NodeFactory();
            IUriNode firstClass = factory.CreateUriNode(UriFactory.Create("http://example.com/ns#CLASS_1"));
            IUriNode secondClass = factory.CreateUriNode(UriFactory.Create("http://example.com/ns#CLASS_2"));
            yield return new TestCaseData(new List<IUriNode>(), 0);
            yield return new TestCaseData(new List<IUriNode>() { firstClass }, 1);
            yield return new TestCaseData(new List<IUriNode>() { firstClass, secondClass }, 2);
        }

        [Test, TestCaseSource(nameof(EntityClassArguments))]
        public void Return_expected_model_with_class_terms(List<IUriNode> classes, int expectedCount)
        {
            Mock.Get(MockPropertyMap).Setup(f => f.ListEntityClasses()).Returns(classes);
            IUriNode entity = factory.CreateUriNode(UriFactory.Create("http://example.com/data/entity"));
            INode node = factory.CreateUriNode(UriFactory.Create("rdf:type"));
            TriplesMapFuture triplesMap = new TriplesMapFuture.Builder(IdName, MockLogicalTable, MockPropertyMap).Build();
            IGraph result = triplesMap.GenerateClassTerms(entity);
            Assert.That(result.Triples.Count, Is.EqualTo(expectedCount));
        }

        public static IEnumerable<TestCaseData> PredicateObjectArguments()
        {
            INodeFactory factory = new NodeFactory();
            Func<TriplesMapFuture.Builder> triplesMapBuilderSupplier = () => new TriplesMapFuture.Builder(string.Empty, MockLogicalTable, MockPropertyMap);
            IRelationMap mockRelationMap = Mock.Of<IRelationMap>();
            INodeMap mockNodeMap = Mock.Of<INodeMap>();
            IUriNode predicate = factory.CreateUriNode(UriFactory.Create("http://example.com/data/PREDICATE"));
            ILiteralNode literal = factory.CreateLiteralNode("VALUE");
            Mock.Get(mockNodeMap).Setup(f => f.GenerateNodeTerm(It.IsAny<IEntity>())).Returns(literal);
            Mock.Get(mockRelationMap).Setup(f => f.GenerateRelationTerm(It.IsAny<IEntity>())).Returns(predicate);
            yield return new TestCaseData(triplesMapBuilderSupplier.Invoke(), 0);
            yield return new TestCaseData(triplesMapBuilderSupplier.Invoke().AddPredicateObjectMap(new KeyValuePair<IRelationMap, INodeMap>(mockRelationMap, mockNodeMap)), 1);
        }

        [Test, TestCaseSource(nameof(PredicateObjectArguments))]
        public void Return_expected_model_with_predicate_object_terms(TriplesMapFuture.Builder builder, int expectedCount)
        {
            IUriNode subject = factory.CreateUriNode(UriFactory.Create("http://example.com/data/SUBJECT"));
            triplesMap = builder.Build();
            IGraph result = triplesMap.GenerateNodeTerms(subject, Mock.Of<IEntity>());
            Assert.That(result.Triples.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void Creating_instance_with_no_unique_id_is_not_possible()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TriplesMapFuture.Builder(null, MockLogicalTable, MockPropertyMap),
                "ID name must not be null."
            );
        }

        [Test]
        public void Creating_instance_with_no_logical_table_is_not_possible()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TriplesMapFuture.Builder("ID_NAME", null, MockPropertyMap),
                "ID name must not be null."
            );
        }

        [Test]
        public void Creating_instance_with_no_subject_map_is_not_possible()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TriplesMapFuture.Builder("ID_NAME", MockLogicalTable, null),
                "Subject map must not be null."
            );
        }
    }
}

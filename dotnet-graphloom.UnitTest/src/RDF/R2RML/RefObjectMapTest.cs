//-----------------------------------------------------------------------
// <copyright file="RefObjectMapTest.cs" company="github.com/jiefenn8">
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
using GraphLoom.Mapper.Util;
using Moq;
using NUnit.Framework;
using VDS.RDF;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    /// <summary>
    /// Unit test class for <see cref="RefObjectMap"/>.
    /// </summary>
    [TestFixture]
    public class RefObjectMapTest
    {
        private RefObjectMap refObjectMap;
        private IEntityMap mockParentTriplesMap;

        [SetUp]
        public void SetUp()
        {
            mockParentTriplesMap = Mock.Of<IEntityMap>();
        }

        [Test]
        public void Generate_expected_term()
        {
            IUriNode expected = Mock.Of<IUriNode>();
            Mock.Get(mockParentTriplesMap).Setup(f => f.GenerateEntityTerm(It.IsAny<IEntity>())).Returns(expected);
            RefObjectMap refObjectMap = new RefObjectMap.Builder(mockParentTriplesMap).Build();
            INode result = refObjectMap.GenerateNodeTerm(Mock.Of<IEntity>());
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Return_valid_guid()
        {
            refObjectMap = new RefObjectMap.Builder(mockParentTriplesMap).Build();
            string guid = refObjectMap.GetUniqueId();
            Guid result = Guid.Parse(guid);
            Assert.That(result.ToString(), Is.EqualTo(guid));
        }

        private static IEnumerable<TestCaseData> joinConditionArguments()
        {
            IEntityMap mockParentTriplesMap = Mock.Of<IEntityMap>();
            yield return new TestCaseData(new RefObjectMap.Builder(mockParentTriplesMap).AddJoinCondition("PARENT", "CHILD"), true);
            yield return new TestCaseData(new RefObjectMap.Builder(mockParentTriplesMap), false);
        }

        [Test, TestCaseSource(nameof(joinConditionArguments))]
        public void Return_expected_evaluation_when_checking_for_join_condition(RefObjectMap.Builder builder, bool expected)
        {
            bool result = builder.Build().HasJoinConditions();
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Return_expected_collection_when_given_join_condition()
        {
            JoinCondition expected = new JoinCondition("PARENT", "CHILD");
            RefObjectMap.Builder builder = new RefObjectMap.Builder(mockParentTriplesMap);
            builder.AddJoinCondition("PARENT", "CHILD");
            ISet<JoinCondition> result = builder.Build().JoinConditions;
            Assert.That(result.Contains(expected), Is.True);
        }

        [Test]
        public void Return_empty_collection_when_no_join_condition_given()
        {
            RefObjectMap.Builder builder = new RefObjectMap.Builder(mockParentTriplesMap);
            ISet<JoinCondition> result = builder.Build().JoinConditions;
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Return_expected_JSON_string_of_instance()
        {
            refObjectMap = new RefObjectMap.Builder(mockParentTriplesMap).Build();
            string expected = JsonHelper.ToJson(refObjectMap);
            string result = refObjectMap.ToString();
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Creating_instance_with_no_parent_triples_map_is_not_possible()
        {
            Assert.Throws<ArgumentNullException>(() => new RefObjectMap.Builder(null).Build());
        }

        private static IEnumerable<TestCaseData> JoinConditionArguments()
        {
            yield return new TestCaseData(null, "CHILD");
            yield return new TestCaseData("PARENT", null);
            yield return new TestCaseData(null, null);
        }

        [Test, TestCaseSource(nameof(JoinConditionArguments))]
        public void Join_condition_with_no_value_is_not_possible(string parent, string child)
        {
            RefObjectMap.Builder builder = new RefObjectMap.Builder(mockParentTriplesMap);
            Assert.Throws<ArgumentNullException>(() => builder.AddJoinCondition(parent, child));
        }

        public void Return_builder_when_adding_join_condition()
        {
            RefObjectMap.Builder result = new RefObjectMap.Builder(mockParentTriplesMap).AddJoinCondition("PARENT", "CHILD");
            Assert.That(result, Is.InstanceOf<RefObjectMap.Builder>());
        }

        [Test]
        public void Return_new_instance_when_build()
        {
            RefObjectMap result = new RefObjectMap.Builder(mockParentTriplesMap).Build();
            Assert.That(result, Is.InstanceOf<RefObjectMap>());
        }
    }
}

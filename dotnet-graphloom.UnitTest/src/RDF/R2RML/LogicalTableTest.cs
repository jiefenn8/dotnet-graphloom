//-----------------------------------------------------------------------
// <copyright file="LogicalTableTest.cs" company="github.com/jiefenn8">
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
using GraphLoom.Mapper.Exceptions;
using GraphLoom.Mapper.RDF.R2RML;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    /// <summary>
    /// Unit test class for <see cref="LogicalTable"/>.
    /// </summary>
    [TestFixture]
    public class LogicalTableTest
    {
        private LogicalTable logicalTable;
        private IEntityReference mockEntityReference;

        [SetUp]
        public void SetUp()
        {
            mockEntityReference = Mock.Of<IEntityReference>();
        }

        [Test]
        public void Creating_instance_with_no_entity_reference_is_not_possible()
        {
            Assert.Throws<ArgumentNullException>(
                () => new LogicalTable.Builder((IEntityReference)null),
                "Payload must not be null."
                );
        }

        [Test]
        public void Return_expected_hash_code()
        {
            int expected = HashCode.Combine(mockEntityReference);
            logicalTable = new LogicalTable.Builder(mockEntityReference).Build();
            int result = logicalTable.GetHashCode();
            Assert.That(result, Is.EqualTo(expected));
        }

        private static IEnumerable<TestCaseData> LogicalTableArguments()
        {
            LogicalTable logicalTable = new LogicalTable.Builder(Mock.Of<IEntityReference>()).Build();
            yield return new TestCaseData(logicalTable, logicalTable, true);
            yield return new TestCaseData(logicalTable, new LogicalTable.Builder(Mock.Of<IEntityReference>()).Build(), false);
        }

        [Test, TestCaseSource(nameof(LogicalTableArguments))]
        public void Return_expected_evaluation_when_compare_instance(LogicalTable subject, LogicalTable value, bool expected)
        {
            bool result = subject.Equals(value);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Return_valid_guid()
        {
            logicalTable = new LogicalTable.Builder(mockEntityReference).Build();
            string guid = logicalTable.GetUniqueId();
            Guid result = Guid.Parse(guid);
            Assert.That(result.ToString(), Is.EqualTo(guid));
        }

        [Test]
        public void Return_expected_JSON_string_of_instance()
        {
            logicalTable = new LogicalTable.Builder(mockEntityReference).Build();
            JsonConvert.SerializeObject(logicalTable);
            string result = logicalTable.ToString();  
            Assert.That(result, Is.EqualTo(result));
        }

        [Test]
        public void Return_expected_entity_reference()
        {
            logicalTable = new LogicalTable.Builder(mockEntityReference).Build();
            IEntityReference result = logicalTable.GetEntityReference();
            Assert.That(result, Is.EqualTo(mockEntityReference));
        }

        [Test]
        public void Return_builder_when_adding_joint_query()
        {
            Mock.Get(mockEntityReference).Setup(f => f.GetPayload()).Returns("PAYLOAD");
            JoinCondition mockJoinCondition = new JoinCondition("parent","child");
            LogicalTable second = new LogicalTable.Builder(mockEntityReference).Build();
            LogicalTable.Builder builder = new LogicalTable.Builder(mockEntityReference);
            LogicalTable.Builder result = builder.WithJointQuery(second, new HashSet<JoinCondition> { mockJoinCondition });
            Assert.That(result, Is.EqualTo(builder));
        }

        [Test]
        public void Joint_query_with_no_join_condition_is_not_possible()
        {
            LogicalTable second = new LogicalTable.Builder(mockEntityReference).Build();
            LogicalTable.Builder builder = new LogicalTable.Builder(Mock.Of<IEntityReference>());
            Assert.Throws<ParserException>(
                () => builder.WithJointQuery(second, new HashSet<JoinCondition>()),
                "Expected JoinConditions with joint query creation."
                ) ;
        }
    }
}

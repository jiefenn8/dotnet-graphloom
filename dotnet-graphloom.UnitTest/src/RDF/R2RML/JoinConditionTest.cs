//-----------------------------------------------------------------------
// <copyright file="JoinConditionTest.cs" company="github.com/jiefenn8">
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
using Newtonsoft.Json;
using NUnit.Framework;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    /// <summary>
    /// Unit test class for <see cref="JoinCondition"/>
    /// </summary>
    [TestFixture]
    public class JoinConditionTest
    {
        private JoinCondition joinCondition;

        [SetUp]
        public void SetUp()
        {
            joinCondition = new JoinCondition("PARENT", "CHILD");
        }

        public static IEnumerable<TestCaseData> InvalidJoinConditionArguments()
        {
            yield return new TestCaseData(null, null);
            yield return new TestCaseData("PARENT", null);
            yield return new TestCaseData(null, "CHILD");
        }

        [Test, TestCaseSource(nameof(InvalidJoinConditionArguments))]
        public void Create_instance_with_a_null_argument_is_not_possible(string parent, string child)
        {
            Assert.Throws<ArgumentNullException>(() => new JoinCondition(parent, child));
        }

        [Test]
        public void Return_expected_join_string()
        {
            string expected = "child.CHILD=parent.PARENT";
            string result = joinCondition.GetJoinString();
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Return_expected_hash_code()
        {
            int expected = HashCode.Combine("PARENT", "CHILD");
            int result = joinCondition.GetHashCode();
            Assert.That(result, Is.EqualTo(expected));
        }


        private static IEnumerable<TestCaseData> JoinConditionArguments()
        {
            JoinCondition joinCondition = new JoinCondition("PARENT", "CHILD");
            yield return new TestCaseData(joinCondition, joinCondition, true);
            yield return new TestCaseData(joinCondition, new JoinCondition("ALT_PARENT", "ALT_CHILD"), false);
        }

        [Test, TestCaseSource(nameof(JoinConditionArguments))]
        public void Return_expected_evaluation_when_compare_instance(JoinCondition subject, JoinCondition value, bool expected)
        {
            bool result = subject.Equals(value);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Return_expected_JSON_string_of_instance()
        {
            JsonConvert.SerializeObject(joinCondition);
            string result = joinCondition.ToString();
            Assert.That(result, Is.EqualTo(result));
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="R2RMLMapTest.cs" company="github.com/jiefenn8">
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
using GraphLoom.Mapper.RDF.R2RML;
using Moq;
using NUnit.Framework;

namespace GraphLoom.UnitTest.RDF.R2RML
{
    /// <summary>
    /// Unit test class for <see cref="R2RMLMap"/> and its builder.
    /// </summary>
    [TestFixture]
    public class R2RMLMapTest
    {
        public R2RMLMap r2rmlMap;

        public static IEnumerable<TestCaseData> NamespaceArguments()
        {
            IDictionary<string, string> defaultNamespaceMap = new Dictionary<string, string>();
            defaultNamespaceMap.Add("rr", "http://www.w3.org/ns/r2rml#");
            IDictionary<string, string> namespaceMap = new Dictionary<string, string>(defaultNamespaceMap);
            namespaceMap.Add("ex", "http://example.com/ns/");
            yield return new TestCaseData(new R2RMLMap.Builder(), defaultNamespaceMap);
            yield return new TestCaseData(new R2RMLMap.Builder().AddNsPrefix("ex", "http://example.com/ns/"), namespaceMap);
        }

        [Test, TestCaseSource(nameof(NamespaceArguments))]
        public void Return_expected_namespace_map(R2RMLMap.Builder builder, IDictionary<string, string> expected) 
        {
            r2rmlMap = builder.Build();
            IDictionary<string, string> result = r2rmlMap.GetNamespaceMap();
            Assert.That(result, Is.EqualTo(expected));
        }

        public static IEnumerable<TestCaseData> TriplesMapArguments()
        {
            IEntityMap mockTriplesMap = Mock.Of<IEntityMap>();
            Func<R2RMLMap.Builder> r2rmlMapBuilderSupplier = () => new R2RMLMap.Builder();
            R2RMLMap.Builder builder = r2rmlMapBuilderSupplier.Invoke();
            ISet<IEntityMap> expected = new HashSet<IEntityMap>();

            //Add triplesmap to builder starting from none to two for each return.
            for(int i = 0; i <= 2; i++)
            {
                yield return new TestCaseData(builder, expected);
                IEntityMap mockEntityMap = Mock.Of<IEntityMap>();
                builder.AddTriplesMap(mockEntityMap);
                expected.Add(mockEntityMap);
            }
        }

        [Test, TestCaseSource (nameof(TriplesMapArguments))]
        public void Return_expected_triples_map_collection(R2RMLMap.Builder builder, ISet<IEntityMap> expected)
        {
            R2RMLMap r2rmlMap = builder.Build();
            Assert.That(r2rmlMap.GetEntityMaps(), Is.EqualTo(expected));
        }

        [Test, TestCaseSource(nameof(TriplesMapArguments))]
        public void Return_expected_enumerator_from_instance(R2RMLMap.Builder builder, ISet<IEntityMap> expected)
        {
            R2RMLMap r2rmlMap = builder.Build();
            IEnumerable<IEntityMap> result = r2rmlMap;
            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}

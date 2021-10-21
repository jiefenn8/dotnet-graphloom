//-----------------------------------------------------------------------
// <copyright file="SubjectMap.cs" company="github.com/jiefenn8">
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
using GraphLoom.Mapper.Core.ConfigMaps;
using GraphLoom.Mapper.Core.InputSource;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF.R2RML
{
    /// <summary>
    /// Implementation of R2RML SubjectMap with <see cref="IPropertyMap"/> 
    /// interface. This term map will return either a rr:IRI or rr:BlankNode 
    /// for its main term.
    /// </summary>
    public class SubjectMap : AbstractTermMap<SubjectMap>, IPropertyMap
    {
        /// <summary>
        /// Unique identifier for this instance.
        /// </summary>
        private readonly Guid uuid = Guid.NewGuid();

        /// <summary>
        /// Collection containing classes associated with this entity.
        /// </summary>
        private readonly ISet<IUriNode> classes = new HashSet<IUriNode>();

        /// <inheritdoc/>
        public SubjectMap(Builder builder) : base(builder) 
        {
            classes = builder.Classes;
        }

        /// <inheritdoc/>
        public IUriNode GenerateEntityTerm(IEntity entity)
        {
            INode rdfTerm = GenerateRDFTerm(entity);
            return rdfTerm == null ? null : (IUriNode)rdfTerm;
        }

        /// <inheritdoc/>
        public List<IUriNode> ListEntityClasses()
        {
            return classes.ToList();
        }

        /// <inheritdoc/>
        protected override INode HandleDefaultGeneration(string term)
        {
            return AsRDFTerm(term, TermType.IRI);
        }

        /// <summary>
        /// Builder class for <see cref="SubjectMap"/>.
        /// </summary>
        public class Builder : AbstractTermMapBuilder<SubjectMap>, ITermMapBuilder<SubjectMap>
        {
            /// <inheritdoc/>
            public Builder(IUriNode baseUri, INode baseValue, ValuedType valuedType) : base(baseUri, baseValue, valuedType) { }

            /// <summary>
            /// Associates a collection of classes with this to the RDF term that
            /// this TermMap will generate.
            /// </summary>
            /// <param name="classes">Set containing unique classes</param>
            /// <returns>this Builder for method chaining</returns>
            public Builder AddEntityClasses(ISet<IUriNode> classes)
            {
                Classes.UnionWith(classes);
                return this;
            }

            /// <summary>
            /// Collection of classes associated with this entity.
            /// </summary>
            public ISet<IUriNode> Classes { get; private set; } = new HashSet<IUriNode>();

            /// <inheritdoc/>
            public override SubjectMap Build()
            {
                return new SubjectMap(this);
            }
        }
    }
}

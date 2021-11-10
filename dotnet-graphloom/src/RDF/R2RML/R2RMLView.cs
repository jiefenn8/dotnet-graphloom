//-----------------------------------------------------------------------
// <copyright file="R2RMLView.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using GraphLoom.Mapper.Core.InputSource;

namespace GraphLoom.Mapper.RDF.R2RML
{
    /// <summary>
    /// This class extends the base methods of the <see cref="BaseEntityReference"/> 
    /// to manage configuration to only handle SQL retrieval through the use of 
    /// custom SQL query.
    /// </summary>
    public class R2RMLView : BaseEntityReference
    {
        /// <summary>
        /// Constructs a R2RMLView with the specified custom SQL query as the source
        /// payload.
        /// </summary>
        /// <param name="builder">the r2rml view builder ot build from</param>
        private R2RMLView(Builder builder) : base(builder.Payload, DatabaseType.QUERY, string.Empty)
        {
            SetProperty("sqlVersion", builder.SqlVersion);
        }

        /// <summary>
        /// Builder class for R2RMLView.
        /// </summary>
        public class Builder
        {
            /// <summary>
            /// The payload string value.
            /// </summary>
            public string Payload { get; }
            
            /// <summary>
            /// The sql version that this payload supports if applicable.
            /// </summary>
            public string SqlVersion { get; }

            /// <summary>
            /// Constructs an instance of Builder with the given payload and
            /// sql version.
            /// </summary>
            /// <param name="payload">the payload containing the info needed to get the view</param>
            /// <param name="sqlVersion">the sql version that this payload supports</param>
            public Builder(string payload, string sqlVersion)
            {
                Payload = payload;
                SqlVersion = sqlVersion;
            }

            /// <summary>
            /// Returns an immutable instance of r2rml view containing the properties
            /// given to its builder.
            /// </summary>
            /// <returns>instance of r2rml view created with the info in this builder</returns>
            public R2RMLView Build()
            {
                return new R2RMLView(this);
            }
        }
    }
}

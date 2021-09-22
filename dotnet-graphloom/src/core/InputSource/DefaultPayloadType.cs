//-----------------------------------------------------------------------
// <copyright file="DefaultPayloadType.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

namespace GraphLoom.Mapper.src.Core.InputSource
{
    /// <summary>
    /// This interface defines the base methods to manage the type of payload
    /// the query or reference can be.
    /// </summary>
    public class PayloadType
    {
        /// <summary>
        /// The default payload type. For internal use only.
        /// </summary>
        public static readonly PayloadType UNDEFINED = new PayloadType("UNDEFINED");

        /// <inheritdoc/>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayloadType"/> class with
        /// the specified type. 
        /// </summary>
        /// <param name="value">The payload type as string</param>
        protected PayloadType(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets payload type value for this instance.
        /// </summary>
        public string Value { get; private set; }
    }
}

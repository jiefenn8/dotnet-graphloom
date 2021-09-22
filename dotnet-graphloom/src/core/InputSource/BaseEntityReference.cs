//-----------------------------------------------------------------------
// <copyright file="BaseEntityReference.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using GraphLoom.Mapper.src.Core.InputSource;

namespace GraphLoom.Mapper.Core.InputSource
{
    /// <summary>
    /// Implementation of <see cref="IEntityReference"/> interface.
    /// </summary>
    public class BaseEntityReference : IEntityReference
    {
        /// <summary>
        /// The payload query to use.
        /// </summary>
        private readonly string _payload;

        /// <summary>
        /// The type of payload given.
        /// </summary>
        private readonly PayloadType _payloadType;

        /// <summary>
        /// The definition to iterate each row of results.
        /// </summary>
        private readonly string _iteratorDef;

        /// <summary>
        /// Additional custom properties for this configuration instance.
        /// </summary>
        private readonly Dictionary<string, string> _properties = new Dictionary<string, string>();

        /// <summary>
        /// Constructs an BaseEntityReference with the specified payload, type
        /// and iterator definition.
        /// </summary>
        /// <param name="payload">the payload query to use</param>
        /// <param name="payloadType">the type of payload given</param>
        /// <param name="iteratorDef">the definition to iterate each row of results</param>
        public BaseEntityReference(string payload, PayloadType payloadType, string iteratorDef)
        {
            if (payload == null) throw new ArgumentNullException("Payload must not be null.");
            if (payloadType == null) throw new ArgumentNullException("Payload type must not be null.");
            if (iteratorDef == null) throw new ArgumentNullException("Iterator definition must not be null.");
            Contract.EndContractBlock();
            _payload = payload;
            _payloadType = payloadType;
            _iteratorDef = iteratorDef;
        }

        /// <inheritdoc/>
        public PayloadType GetPayloadType()
        {
            return _payloadType;
        }

        /// <inheritdoc/>
        public string GetPayload()
        {
            return _payload;
        }

        /// <inheritdoc/>
        public string GetIteratorDef()
        {
            return _iteratorDef;
        }

        /// <summary>
        /// Sets a custom property to this config with given property name
        /// and value.Returns the value of the previous value of the
        /// associated key or null if there was no value set for this property.
        /// </summary>
        /// <param name="key">the name of the property to add to this config</param>
        /// <param name="value">the value to associate to the given property</param>
        public void SetProperty(string key, string value)
        {
            _properties[key] = value;
        }

        /// <inheritdoc/>
        public string GetProperty(string key)
        {
            string value = _properties[key];
            if (value == null)
            {
                return string.Empty;
            }
            return value;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is BaseEntityReference reference &&
                   _payload == reference._payload &&
                   EqualityComparer<PayloadType>.Default.Equals(_payloadType, reference._payloadType) &&
                   _iteratorDef == reference._iteratorDef &&
                   EqualityComparer<Dictionary<string, string>>.Default.Equals(_properties, reference._properties);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(_payload, _payloadType, _iteratorDef, _properties);
        }
    }
}

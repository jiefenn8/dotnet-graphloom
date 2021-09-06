using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using GraphLoom.Mapper.src.Core.InputSource;

namespace GraphLoom.Mapper.Core.InputSource
{
    public class BaseEntityReference : IEntityReference
    {
        private readonly string _payload;
        private readonly PayloadType _payloadType;
        private readonly string _iteratorDef;
        private readonly Dictionary<string, string> _properties = new Dictionary<string, string>();

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

        public PayloadType GetPayloadType()
        {
            return _payloadType;
        }

        public string GetPayload()
        {
            return _payload;
        }

        public string GetIteratorDef()
        {
            return _iteratorDef;
        }

        public void SetProperty(string key, string value)
        {
            _properties[key] = value;
        }

        public string GetProperty(string key)
        {
            string value = _properties[key];
            if (value == null)
            {
                return string.Empty;
            }
            return value;
        }

        public override bool Equals(object obj)
        {
            return obj is BaseEntityReference reference &&
                   _payload == reference._payload &&
                   EqualityComparer<PayloadType>.Default.Equals(_payloadType, reference._payloadType) &&
                   _iteratorDef == reference._iteratorDef &&
                   EqualityComparer<Dictionary<string, string>>.Default.Equals(_properties, reference._properties);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_payload, _payloadType, _iteratorDef, _properties);
        }
    }
}

using GraphLoom.Mapper.Core.InputSource;
using GraphLoom.Mapper.src.Core.InputSource;
using NUnit.Framework;
using System;

namespace GraphLoom.UnitTest.Core.InputSource
{
    [TestFixture]
    public class BaseEntityReferenceTest
    {
        private const string _payload = "PAYLOAD";
        private const string _iteratorDef = "";
        private BaseEntityReference _baseEntityReference;

        static object[] payloadTypeArguments =
        {
            new object[] {PayloadType.UNDEFINED}
        };

        [TestCaseSource(nameof(payloadTypeArguments))]
        public void Creating_instance_with_no_payload_is_not_possible(PayloadType payloadType)
        {
            Assert.Throws<ArgumentNullException>(
                () => new BaseEntityReference(null, payloadType, _iteratorDef),
                "Payload must not be null."
                );
        }

        [Test]
        public void Creating_instance_with_no_payload_type_is_not_possible()
        {
            Assert.Throws<ArgumentNullException>(
                () => new BaseEntityReference(_payload, null, _iteratorDef),
                "Payload type must not be null."
                );
        }

        [TestCaseSource(nameof(payloadTypeArguments))]
        public void Creating_instance_with_no_iterator_definition_is_not_possible(PayloadType payloadType)
        {
            Assert.Throws<ArgumentNullException>(
                () => new BaseEntityReference(_payload, payloadType, null),
                "Iterator definition must not be null."
                );
        }

        [TestCaseSource(nameof(payloadTypeArguments))]
        public void Return_payload_type(PayloadType payloadType)
        {
            _baseEntityReference = new BaseEntityReference(_payload, payloadType, _iteratorDef);
            PayloadType result = _baseEntityReference.GetPayloadType();
            Assert.That(result, Is.Not.Null);
        }

        [TestCaseSource(nameof(payloadTypeArguments))]
        public void Return_non_empty_payload_string(PayloadType payloadType)
        {
            _baseEntityReference = new BaseEntityReference(_payload, payloadType, _iteratorDef);
            string result = _baseEntityReference.GetPayload();
            Assert.That(result, Is.Not.Empty);
        }

        [TestCaseSource(nameof(payloadTypeArguments))]
        public void Return_iterator_definition_string(PayloadType payloadType)
        {
            _baseEntityReference = new BaseEntityReference(_payload, payloadType, _iteratorDef);
            string result = _baseEntityReference.GetIteratorDef();
            Assert.That(result, Is.Not.Null);
        }

        [TestCaseSource(nameof(payloadTypeArguments))]
        public void Return_value_of_property_match(PayloadType payloadType)
        {
            _baseEntityReference = new BaseEntityReference(_payload, payloadType, _iteratorDef);
            _baseEntityReference.SetProperty("PROPERTY", "VALUE");
            string result = _baseEntityReference.GetProperty("PROPERTY");
            Assert.That(result, Is.EqualTo("VALUE"));
        }

        [TestCaseSource(nameof(payloadTypeArguments))]
        public void Return_empty_value_if_property_match_value_not_found(PayloadType payloadType)
        {
            _baseEntityReference = new BaseEntityReference(_payload, payloadType, _iteratorDef);
            _baseEntityReference.SetProperty("PROPERTY", null);
            string result = _baseEntityReference.GetProperty("PROPERTY");
            Assert.That(result, Is.Empty);
        }

        [TestCaseSource(nameof(payloadTypeArguments))]
        public void Set_property_with_no_key_is_not_possible(PayloadType payloadType)
        {
            _baseEntityReference = new BaseEntityReference(_payload, payloadType, _iteratorDef);
            Assert.Throws<ArgumentNullException>(() => _baseEntityReference.SetProperty(null, null));
        }
    }
}

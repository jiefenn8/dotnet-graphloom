namespace GraphLoom.Mapper.src.Core.InputSource
{
    public class PayloadType
    {
        public static readonly PayloadType UNDEFINED = new PayloadType("UNDEFINED");

        public override string ToString()
        {
            return Value;
        }

        protected PayloadType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}

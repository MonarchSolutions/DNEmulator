using DNEmulator.Abstractions;
using DNEmulator.Enumerations;

namespace DNEmulator.Values
{
    public class ObjectValue : Value
    {
        public override DNValueType ValueType => DNValueType.Object;

        public object Value { get; }
        public ObjectValue(object value)
        {
            Value = value;
        }
    }
}

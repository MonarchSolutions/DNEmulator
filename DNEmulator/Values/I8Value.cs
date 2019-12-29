using DNEmulator.Abstractions;
using DNEmulator.Enumerations;

namespace DNEmulator.Values
{
    public class I8Value : Value
    {
        public override DNValueType ValueType => DNValueType.Int64;

        public long Value { get; }

        public I8Value(long value)
        {
            Value = value;
        }
    }
}

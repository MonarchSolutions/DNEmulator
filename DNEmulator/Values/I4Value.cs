using DNEmulator.Abstractions;
using DNEmulator.Enumerations;

namespace DNEmulator.Values
{
    public class I4Value : Value
    {
        public override DNValueType ValueType => DNValueType.Int32;

        public int Value { get; }

        public I4Value(int value)
        {
            Value = value;
        }
    }
}

using DNEmulator.Abstractions;

namespace DNEmulator.Values
{
    public class I4Value : Value
    {
        public override DNValueType ValueType => DNValueType.Int32;

        public I4Value(int value)
        {
            Value = value;
        }

        public int Value
        {
            get;
        }
    }
}

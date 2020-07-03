using DNEmulator.Abstractions;


namespace DNEmulator.Values
{
    public class I8Value : Value
    {
        public override DNValueType ValueType => DNValueType.Int64;

        public I8Value(long value)
        {
            Value = value;
        }

        public long Value
        {
            get;
        }

    }
}

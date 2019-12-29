using DNEmulator.Abstractions;
using DNEmulator.Enumerations;

namespace DNEmulator.Values
{
    public class StringValue : Value
    {
        public override DNValueType ValueType => DNValueType.String;

        public string Value { get; }

        public StringValue(string value)
        {
            Value = value;
        }
    }
}

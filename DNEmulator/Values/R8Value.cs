using DNEmulator.Abstractions;
using DNEmulator.Enumerations;

namespace DNEmulator.Values
{
    public class R8Value : Value
    {
        public override DNValueType ValueType => DNValueType.Real;

        public double Value { get; }
        public R8Value(double value)
        {
            Value = value;
        }
    }
}

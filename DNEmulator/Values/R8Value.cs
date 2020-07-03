using DNEmulator.Abstractions;


namespace DNEmulator.Values
{
    public class R8Value : Value
    {
        public override DNValueType ValueType => DNValueType.Real;

        public R8Value(double value)
        {
            Value = value;
        }
        public double Value
        {
            get;
        }

    }
}

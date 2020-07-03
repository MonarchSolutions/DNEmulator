using DNEmulator.Abstractions;


namespace DNEmulator.Values
{
    public class ObjectValue : Value
    {
        public override DNValueType ValueType => DNValueType.Object;

        public ObjectValue(object value)
        {
            Value = value;
        }

        public object Value
        {
            get;
        }
    }
}

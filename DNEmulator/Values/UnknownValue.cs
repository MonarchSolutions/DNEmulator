using DNEmulator.Abstractions;
using dnlib.DotNet;

namespace DNEmulator.Values
{
    public class UnknownValue : Value
    {
        public override DNValueType ValueType => DNValueType.Unknown;

        public UnknownValue(ElementType expectedType)
        {
            ExpectedType = expectedType;
        }

        public ElementType ExpectedType
        {
            get;
        }
    }
}

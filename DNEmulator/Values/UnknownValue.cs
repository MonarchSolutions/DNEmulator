using DNEmulator.Abstractions;
using DNEmulator.Enumerations;
using dnlib.DotNet;

namespace DNEmulator.Values
{
    public class UnknownValue : Value
    {
        public override DNValueType ValueType => DNValueType.Unknown;

        public ElementType ExpectedType { get; }

        public UnknownValue(ElementType expectedType)
        {
            ExpectedType = expectedType;
        }

    }
}

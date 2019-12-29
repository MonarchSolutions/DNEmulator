
using DNEmulator.Enumerations;

namespace DNEmulator.Abstractions
{
    public abstract class Value
    {
        public abstract DNValueType ValueType { get; }
        public bool IsInt32() => ValueType == DNValueType.Int32;
        public bool IsInt64() => ValueType == DNValueType.Int64;
        public bool IsInteger() =>  ValueType == DNValueType.Int32 || ValueType == DNValueType.Int64;
        public bool IsRealValue() => ValueType == DNValueType.Real;
        public bool IsString() => ValueType == DNValueType.String;
        public bool IsObject() => ValueType == DNValueType.Object;
        public bool IsUnknown() => ValueType == DNValueType.Unknown;
    }
}

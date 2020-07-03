using DNEmulator.Values;
using System;

namespace DNEmulator.Abstractions
{
    public abstract class Value
    {
        public abstract DNValueType ValueType { get; }
        public bool IsInt32() => ValueType == DNValueType.Int32;
        public bool IsInt64() => ValueType == DNValueType.Int64;
        public bool IsInteger() => ValueType == DNValueType.Int32 || ValueType == DNValueType.Int64;
        public bool IsRealValue() => ValueType == DNValueType.Real;
        public bool IsObject() => ValueType == DNValueType.Object;
        public bool IsUnknown() => ValueType == DNValueType.Unknown;

        public bool IsString(out string str)
        {
            str = null;
            if (!(this is ObjectValue objectValue && objectValue.Value is string))
                return false;

            str = (string)objectValue.Value;
            return true;
        }

        public object GetValue() => ValueType switch
        {
            DNValueType.Int32 => ((I4Value)this).Value,
            DNValueType.Int64 => ((I8Value)this).Value,
            DNValueType.Real => ((R8Value)this).Value,
            DNValueType.Object => ((ObjectValue)this).Value,
            DNValueType.Native => ((NativeValue)this).Value,
            _ => null,
        };


        public static Value FromObject(object value) => Type.GetTypeCode(value.GetType()) switch
        {
            TypeCode.Boolean => new I4Value(((bool)value) ? 1 : 0),
            TypeCode.Char => new I4Value((char)value),
            TypeCode.Byte => new I4Value((byte)value),
            TypeCode.Double => new R8Value((double)value),
            TypeCode.Int16 => new I4Value((short)value),
            TypeCode.Int32 => new I4Value((int)value),
            TypeCode.Int64 => new I8Value((long)value),
            TypeCode.SByte => new I4Value((sbyte)value),
            TypeCode.Single => new R8Value((float)value),
            TypeCode.UInt16 => new I4Value((ushort)value),
            TypeCode.UInt32 => new I4Value((int)(uint)value),
            TypeCode.UInt64 => new I8Value((long)(ulong)value),
            _ => new ObjectValue(value),
        };





    }
}

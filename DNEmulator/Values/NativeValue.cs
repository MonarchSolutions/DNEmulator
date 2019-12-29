

using DNEmulator.Abstractions;
using DNEmulator.Enumerations;
using System;

namespace DNEmulator.Values
{
    public class NativeValue : Value
    {
        public override DNValueType ValueType => DNValueType.Native;

        public IntPtr Value { get; }

        public NativeValue(IntPtr value)
        {
            Value = value;
        }
         
    }
}

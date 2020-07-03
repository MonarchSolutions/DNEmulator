

using DNEmulator.Abstractions;
using System;

namespace DNEmulator.Values
{
    public class NativeValue : Value
    {
        public override DNValueType ValueType => DNValueType.Native;

        public NativeValue(IntPtr value)
        {
            Value = value;
        }

        public IntPtr Value
        {
            get;
        }

    }
}

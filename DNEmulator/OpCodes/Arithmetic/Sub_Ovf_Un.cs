using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Arithmetic
{
    public class Sub_Ovf_Un : OpCodeEmulator
    {
        public override Code Code => Code.Sub_Ovf_Un;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            checked
            {
                switch (firstValue.ValueType)
                {
                    case DNValueType.Int32 when secondValue.ValueType == DNValueType.Int32:
                        ctx.Stack.Push(new I4Value((int)((uint)((I4Value)firstValue).Value - (uint)((I4Value)secondValue).Value)));
                        break;
                    case DNValueType.Int32 when secondValue.ValueType == DNValueType.Native:
                        ctx.Stack.Push(new NativeValue(IntPtr.Subtract(((NativeValue)secondValue).Value, (int)(uint)((I4Value)firstValue).Value)));
                        break;
                    case DNValueType.Int64 when secondValue.ValueType == DNValueType.Int64:
                        ctx.Stack.Push(new I8Value((long)((ulong)((I8Value)firstValue).Value - (ulong)((I8Value)secondValue).Value)));
                        break;
                    case DNValueType.Native when secondValue.ValueType == DNValueType.Native:
                        ctx.Stack.Push(new NativeValue(new IntPtr((long)((NativeValue)firstValue).Value - (long)((NativeValue)secondValue).Value)));
                        break;
                    case DNValueType.Native when secondValue.ValueType == DNValueType.Int32:
                        ctx.Stack.Push(new NativeValue(IntPtr.Subtract(((NativeValue)firstValue).Value, (int)(uint)((I4Value)secondValue).Value)));
                        break;
                    default:
                        throw new InvalidStackException();
                }
            }

            return new NormalResult();
        }
    }
}

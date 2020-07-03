using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Arithmetic
{
    public class Mul : OpCodeEmulator
    {
        public override Code Code => Code.Mul;
        public override EmulationRequirements Requirements => EmulationRequirements.None;


        public override EmulationResult Emulate(Context ctx)
        {
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            switch (firstValue.ValueType)
            {
                case DNValueType.Int32 when secondValue.ValueType == DNValueType.Int32:
                    ctx.Stack.Push(new I4Value(((I4Value)firstValue).Value * ((I4Value)secondValue).Value));
                    break;
                case DNValueType.Int32 when secondValue.ValueType == DNValueType.Native:
                    ctx.Stack.Push(new NativeValue(new IntPtr(((I4Value)firstValue).Value * (long)((NativeValue)secondValue).Value)));
                    break;
                case DNValueType.Int64 when secondValue.ValueType == DNValueType.Int64:
                    ctx.Stack.Push(new I8Value(((I8Value)firstValue).Value * ((I8Value)secondValue).Value));
                    break;
                case DNValueType.Real when secondValue.ValueType == DNValueType.Real:
                    ctx.Stack.Push(new R8Value(((R8Value)firstValue).Value * ((R8Value)secondValue).Value));
                    break;
                case DNValueType.Native when secondValue.ValueType == DNValueType.Native:
                    ctx.Stack.Push(new NativeValue(new IntPtr((long)((NativeValue)firstValue).Value * (long)((NativeValue)secondValue).Value)));
                    break;
                case DNValueType.Native when secondValue.ValueType == DNValueType.Int32:
                    ctx.Stack.Push(new NativeValue(new IntPtr((long)((NativeValue)firstValue).Value * ((I4Value)secondValue).Value)));
                    break;
                default:
                    throw new InvalidStackException();
            }

            return new NormalResult();
        }
    }
}

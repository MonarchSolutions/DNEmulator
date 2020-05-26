using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Arithmetic
{
    public class Neg : IOpCode
    {
        public Code Code => Code.Neg;

        public EmulationResult Emulate(Context ctx)
        {
            var firstValue = ctx.Stack.Pop();
            switch (firstValue.ValueType)
            {
                case DNValueType.Int32:
                    ctx.Stack.Push(new I4Value(-((I4Value)firstValue).Value));
                    break;
                case DNValueType.Int64:
                    ctx.Stack.Push(new I8Value(-((I8Value)firstValue).Value));
                    break;
                case DNValueType.Real:
                    ctx.Stack.Push(new R8Value(-((R8Value)firstValue).Value));
                    break;
                case DNValueType.Native:
                    ctx.Stack.Push(new NativeValue(new IntPtr(-(long)((NativeValue)firstValue).Value)));
                    break;
                default:
                    throw new InvalidILException(ctx.Instruction.ToString());
            }

            return new NormalResult();
        }
    }
}

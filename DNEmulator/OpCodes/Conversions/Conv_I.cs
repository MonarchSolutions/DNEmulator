using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Conversions
{
    public class Conv_I : IOpCode
    {
        public Code Code => Code.Conv_I;

        public EmulationResult Emulate(Context ctx)
        {
            var firstValue = ctx.Stack.Pop();

            switch (firstValue.ValueType)
            {
                case DNValueType.Int32:
                    ctx.Stack.Push(new NativeValue((IntPtr)((I4Value)firstValue).Value));
                    break;
                case DNValueType.Int64:
                    ctx.Stack.Push(new NativeValue((IntPtr)((I8Value)firstValue).Value));
                    break;
                case DNValueType.Real:
                    ctx.Stack.Push(new NativeValue((IntPtr)((R8Value)firstValue).Value));
                    break;
                default:
                    throw new InvalidILException(ctx.Instruction.ToString());
            }

            return new NormalResult();
        }
    }
}

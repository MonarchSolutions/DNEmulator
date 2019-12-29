using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Arithmetic
{
    public class Shr_Un : IOpCode
    {
        public Code Code => Code.Shr_Un;

        public EmulationResult Emulate(Context ctx)
        {
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            if (!(secondValue is I4Value bitAmount))
                throw new InvalidILException(ctx.Instruction.ToString());

            switch (firstValue.ValueType)
            {
                case DNValueType.Int32:
                    ctx.Stack.Push(new I4Value((int)((uint)((I4Value)firstValue).Value >> bitAmount.Value)));
                    break;
                case DNValueType.Int64:
                    ctx.Stack.Push(new I8Value((int)((ulong)((I8Value)firstValue).Value >> bitAmount.Value)));
                    break;
                case DNValueType.Native:
                    throw new NotSupportedException();
                default:
                    throw new InvalidILException(ctx.Instruction.ToString());
            }

            return new NormalResult();
        }
    }
}

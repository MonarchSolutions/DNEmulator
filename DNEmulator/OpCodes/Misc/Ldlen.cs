using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Misc
{
    public class Ldlen : IOpCode
    {
        public Code Code => Code.Ldlen;

        public EmulationResult Emulate(Context ctx)
        {
            var firstValue = ctx.Stack.Pop();
            switch(firstValue.ValueType)
            {
                case DNValueType.Object when ((ObjectValue)firstValue).Value is Array array:
                    ctx.Stack.Push(new I4Value(array.Length));
                    break;
                case DNValueType.String:
                    ctx.Stack.Push(new I4Value(((StringValue)firstValue).Value.Length));
                    break;
                default:
                    throw new InvalidILException(ctx.Instruction.ToString());
            }

            return new NormalResult();
        }
    }
}

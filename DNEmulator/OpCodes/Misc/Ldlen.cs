using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Misc
{
    public class Ldlen : OpCodeEmulator
    {
        public override Code Code => Code.Ldlen;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            var value = ctx.Stack.Pop();
            switch (value.ValueType)
            {
                case DNValueType.Object when ((ObjectValue)value).Value is Array array:
                    ctx.Stack.Push(new I4Value(array.Length));
                    break;
                case DNValueType.Object when ((ObjectValue)value).Value is string @string:
                    ctx.Stack.Push(new I4Value(@string.Length));
                    break;
                default:
                    throw new InvalidILException(ctx.Instruction.ToString());
            }

            return new NormalResult();
        }
    }
}

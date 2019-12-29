using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Arrays
{
    public class Stelem_I : IOpCode
    {
        public Code Code => Code.Stelem_I;

        public EmulationResult Emulate(Context ctx)
        {
            var thirdValue = ctx.Stack.Pop();
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            if (!(firstValue is ObjectValue obj && obj.Value is IntPtr[] array && secondValue is I4Value index && thirdValue is NativeValue newValue))
                throw new InvalidILException(ctx.Instruction.ToString());

            array[index.Value] = newValue.Value;
            return new NormalResult();
        }
    }
}

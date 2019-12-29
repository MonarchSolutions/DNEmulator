using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Arrays
{
    public class Stelem_I2 : IOpCode
    {
        public Code Code => Code.Stelem_I2;

        public EmulationResult Emulate(Context ctx)
        {
            var thirdValue = ctx.Stack.Pop();
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            if (!(firstValue is ObjectValue obj && obj.Value is Array array && secondValue is I4Value index && thirdValue is I4Value newValue))
                throw new InvalidILException(ctx.Instruction.ToString());

            if (!(array is short[] || array is ushort[]))
                throw new InvalidILException(ctx.Instruction.ToString());

            array.SetValue(newValue.Value, index.Value);

            return new NormalResult();
        }
    }
}

using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Arrays
{
    public class Stelem_R8 : IOpCode
    {
        public Code Code => Code.Stelem_R8;

        public EmulationResult Emulate(Context ctx)
        {
            var thirdValue = ctx.Stack.Pop();
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            if (!(firstValue is ObjectValue obj && obj.Value is double[] array && secondValue is I4Value index && thirdValue is R8Value newValue))
                throw new InvalidILException(ctx.Instruction.ToString());

            array[index.Value] = newValue.Value;

            return new NormalResult();
        }
    }
}

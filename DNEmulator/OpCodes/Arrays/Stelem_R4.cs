using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Arrays
{
    public class Stelem_R4 : OpCodeEmulator
    {
        public override Code Code => Code.Stelem_R4;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            var thirdValue = ctx.Stack.Pop();
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            if (!(firstValue is ObjectValue obj && obj.Value is float[] array && secondValue is I4Value index && thirdValue is R8Value newValue))
                throw new InvalidStackException();

            array[index.Value] = (float)newValue.Value;

            return new NormalResult();
        }
    }
}

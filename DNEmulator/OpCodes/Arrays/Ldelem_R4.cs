using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Arrays
{
    public class Ldelem_R4 : OpCodeEmulator
    {
        public override Code Code => Code.Ldelem_R4;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            if (!(firstValue is ObjectValue obj && obj.Value is float[] array && secondValue is I4Value index))
                throw new InvalidStackException();

            ctx.Stack.Push(new R8Value(array[index.Value]));

            return new NormalResult();
        }
    }
}

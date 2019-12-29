using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Arrays
{
    public class Ldelem_I4 : IOpCode
    {
        public Code Code => Code.Ldelem_I4;

        public EmulationResult Emulate(Context ctx)
        {
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            if (!(firstValue is ObjectValue obj && obj.Value is int[] array && secondValue is I4Value index))
                throw new InvalidILException(ctx.Instruction.ToString());

            ctx.Stack.Push(new I4Value(array[index.Value]));

            return new NormalResult();
        }
    }
}

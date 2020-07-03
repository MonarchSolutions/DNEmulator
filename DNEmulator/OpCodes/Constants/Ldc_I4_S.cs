using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Constants
{
    public class Ldc_I4_S : OpCodeEmulator
    {
        public override Code Code => Code.Ldc_I4_S;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(new I4Value((sbyte)ctx.Instruction.Operand));
            return new NormalResult();
        }
    }
}

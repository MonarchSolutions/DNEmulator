using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Constants
{
    public class Ldstr : OpCodeEmulator
    {
        public override Code Code => Code.Ldstr;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(new ObjectValue((string)ctx.Instruction.Operand));
            return new NormalResult();
        }
    }
}
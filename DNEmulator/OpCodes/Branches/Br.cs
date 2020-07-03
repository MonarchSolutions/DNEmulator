using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using dnlib.DotNet.Emit;


namespace DNEmulator.OpCodes.Branches
{
    public class Br : OpCodeEmulator
    {
        public override Code Code => Code.Br;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            return new JumpResult(ctx.Emulator.Method.Body.Instructions.IndexOf((Instruction)ctx.Instruction.Operand));
        }
    }
}

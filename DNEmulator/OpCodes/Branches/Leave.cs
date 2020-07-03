using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using dnlib.DotNet.Emit;


namespace DNEmulator.OpCodes.Branches
{
    public class Leave : OpCodeEmulator
    {
        public override Code Code => Code.Leave;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Clear();
            return new JumpResult(ctx.Emulator.Method.Body.Instructions.IndexOf((Instruction)ctx.Instruction.Operand));
        }
    }
}

using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;


namespace DNEmulator.OpCodes.Branches
{
    public class Br : IOpCode
    {
        public Code Code => Code.Br;

        public EmulationResult Emulate(Context ctx)
        {
            return new JumpResult(ctx.Emulator.Method.Body.Instructions.IndexOf((Instruction)ctx.Instruction.Operand));
        }
    }
}

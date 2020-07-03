using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Stloc : OpCodeEmulator
    {
        public override Code Code => Code.Stloc;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Instruction.Operand is Local local))
                throw new InvalidILException(ctx.Instruction.ToString());

            ctx.Emulator.LocalMap.Set(local, ctx.Stack.Pop());
            return new NormalResult();
        }
    }
}

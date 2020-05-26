using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Stloc : IOpCode
    {
        public Code Code => Code.Stloc;

        public EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Instruction.Operand is Local local))
                throw new InvalidILException(ctx.Instruction.ToString());

            ctx.Emulator.LocalMap.Set(local, ctx.Stack.Pop());
            return new NormalResult();
        }
    }
}

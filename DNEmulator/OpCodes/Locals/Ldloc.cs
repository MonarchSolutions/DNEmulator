using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Ldloc : IOpCode
    {
        public Code Code => Code.Ldloc;

        public EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Instruction.Operand is Local local))
                throw new InvalidILException(ctx.Instruction.ToString());

            ctx.Stack.Push(ctx.Emulator.LocalMap.Get(local));
            return new NormalResult();
        }
    }
}

using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Ldloc : IOpCode
    {
        public Code Code => Code.Ldloc;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(ctx.Emulator.LocalMap.Get((Local)ctx.Instruction.Operand));
            return new NormalResult();
        }
    }
}

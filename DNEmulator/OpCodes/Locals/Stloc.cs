using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Stloc : IOpCode
    {
        public Code Code => Code.Stloc;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Emulator.LocalMap.Set((Local)ctx.Instruction.Operand, ctx.Stack.Pop());
            return new NormalResult();
        }
    }
}

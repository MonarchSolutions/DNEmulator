using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Parameters
{
    public class Starg : OpCodeEmulator
    {
        public override Code Code => Code.Starg;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            ctx.Emulator.ParameterMap.Set((Parameter)ctx.Instruction.Operand, ctx.Stack.Pop());
            return new NormalResult();
        }
    }
}

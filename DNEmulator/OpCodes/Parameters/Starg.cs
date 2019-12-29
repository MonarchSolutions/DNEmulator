using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Parameters
{
    public class Starg : IOpCode
    {
        public Code Code => Code.Starg;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Emulator.ParameterMap.Set((Parameter)ctx.Instruction.Operand, ctx.Stack.Pop());
            return new NormalResult();
        }
    }
}

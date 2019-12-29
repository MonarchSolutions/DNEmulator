using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Parameters
{
    public class Ldarg : IOpCode
    {
        public Code Code => Code.Ldarg;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(ctx.Emulator.ParameterMap.Get((Parameter)ctx.Instruction.Operand));
            return new NormalResult();
        }

    }
}

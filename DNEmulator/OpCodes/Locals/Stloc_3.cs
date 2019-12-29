using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Stloc_3 : IOpCode
    {
        public Code Code => Code.Stloc_3;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Emulator.LocalMap.Set(ctx.Emulator.Method.Body.Variables[3], ctx.Stack.Pop());
            return new NormalResult();
        }
    }
}

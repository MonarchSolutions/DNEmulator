using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Stloc_0 : IOpCode
    {
        public Code Code => Code.Stloc_0;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Emulator.LocalMap.Set(ctx.Emulator.Method.Body.Variables[0], ctx.Stack.Pop());
            return new NormalResult();
        }
    }
}

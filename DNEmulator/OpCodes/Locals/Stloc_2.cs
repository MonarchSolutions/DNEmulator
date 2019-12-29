using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Stloc_2 : IOpCode
    {
        public Code Code => Code.Stloc_2;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Emulator.LocalMap.Set(ctx.Emulator.Method.Body.Variables[2], ctx.Stack.Pop());
            return new NormalResult();
        }
    }
}

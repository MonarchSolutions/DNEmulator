using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Stloc_0 : OpCodeEmulator
    {
        public override Code Code => Code.Stloc_0;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            ctx.Emulator.LocalMap.Set(ctx.Emulator.Method.Body.Variables[0], ctx.Stack.Pop());
            return new NormalResult();
        }
    }
}

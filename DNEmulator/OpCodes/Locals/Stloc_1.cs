using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Stloc_1 : OpCodeEmulator
    {
        public override Code Code => Code.Stloc_1;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            ctx.Emulator.LocalMap.Set(ctx.Emulator.Method.Body.Variables[1], ctx.Stack.Pop());
            return new NormalResult();
        }
    }
}

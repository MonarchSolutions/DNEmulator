using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Ldloc_0 : OpCodeEmulator
    {
        public override Code Code => Code.Ldloc_0;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(ctx.Emulator.LocalMap.Get(ctx.Emulator.Method.Body.Variables[0]));
            return new NormalResult();
        }
    }
}


using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Parameters
{
    public class Ldarg_1 : OpCodeEmulator
    {
        public override Code Code => Code.Ldarg_1;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(ctx.Emulator.ParameterMap.Get(ctx.Emulator.Method.Parameters[1]));
            return new NormalResult();
        }

    }
}

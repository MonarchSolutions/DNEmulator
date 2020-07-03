using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Parameters
{
    public class Ldarg_3 : OpCodeEmulator
    {
        public override Code Code => Code.Ldarg_3;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(ctx.Emulator.ParameterMap.Get(ctx.Emulator.Method.Parameters[3]));
            return new NormalResult();
        }

    }
}

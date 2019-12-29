using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Parameters
{
    public class Ldarg_0 : IOpCode
    {
        public Code Code => Code.Ldarg_0;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(ctx.Emulator.ParameterMap.Get(ctx.Emulator.Method.Parameters[0]));
            return new NormalResult();
        }

    }
}

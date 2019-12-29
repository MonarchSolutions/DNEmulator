using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Parameters
{
    public class Ldarg_2 : IOpCode
    {
        public Code Code => Code.Ldarg_2;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(ctx.Emulator.ParameterMap.Get(ctx.Emulator.Method.Parameters[2]));
            return new NormalResult();
        }

    }
}

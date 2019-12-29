using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Ldloc_3 : IOpCode
    {
        public Code Code => Code.Ldloc_3;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(ctx.Emulator.LocalMap.Get(ctx.Emulator.Method.Body.Variables[3]));
            return new NormalResult();
        }
    }
}


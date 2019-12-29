using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Ldloc_2 : IOpCode
    {
        public Code Code => Code.Ldloc_2;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(ctx.Emulator.LocalMap.Get(ctx.Emulator.Method.Body.Variables[2]));
            return new NormalResult();
        }
    }
}


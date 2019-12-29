using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Locals
{
    public class Ldloc_1 : IOpCode
    {
        public Code Code => Code.Ldloc_1;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(ctx.Emulator.LocalMap.Get(ctx.Emulator.Method.Body.Variables[1]));
            return new NormalResult();
        }
    }
}


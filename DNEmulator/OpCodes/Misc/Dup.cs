using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Misc
{
    public class Dup : IOpCode
    {
        public Code Code => Code.Dup;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(ctx.Stack.Peek());
            return new NormalResult();
        }
    }
}

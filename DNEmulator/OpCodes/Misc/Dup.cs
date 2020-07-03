using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Misc
{
    public class Dup : OpCodeEmulator
    {
        public override Code Code => Code.Dup;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(ctx.Stack.Peek());
            return new NormalResult();
        }
    }
}

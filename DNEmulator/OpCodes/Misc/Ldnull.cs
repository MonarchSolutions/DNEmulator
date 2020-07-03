using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Misc
{
    public class Ldnull : OpCodeEmulator
    {
        public override Code Code => Code.Ldnull;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(new ObjectValue(null));
            return new NormalResult();
        }
    }
}

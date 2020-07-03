using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Misc
{
    public class Nop : OpCodeEmulator
    {
        public override Code Code => Code.Nop;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            return new NormalResult();
        }
    }
}

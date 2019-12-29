using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Misc
{
    public class Nop : IOpCode
    {
        public Code Code => Code.Nop;

        public EmulationResult Emulate(Context ctx)
        {
            return new NormalResult();
        }
    }
}

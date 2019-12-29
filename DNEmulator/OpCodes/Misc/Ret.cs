using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Misc
{
    public class Ret : IOpCode
    {
        public Code Code => Code.Ret;

        public EmulationResult Emulate(Context ctx)
        {
            return new ReturnResult();
        }
    }
}

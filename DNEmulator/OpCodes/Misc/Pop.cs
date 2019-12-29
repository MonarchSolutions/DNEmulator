using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Misc
{
    public class Pop : IOpCode
    {
        public Code Code => Code.Pop;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Pop();
            return new NormalResult();
        }
    }
}

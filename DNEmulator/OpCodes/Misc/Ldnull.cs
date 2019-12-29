using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Misc
{
    public class Ldnull : IOpCode
    {
        public Code Code => Code.Ldnull;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(new ObjectValue(null));
            return new NormalResult();
        }
    }
}

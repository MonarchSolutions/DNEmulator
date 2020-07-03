using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Constants
{
    public class Ldc_I4_8 : OpCodeEmulator
    {
        public override Code Code => Code.Ldc_I4_8;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(new I4Value(8));
            return new NormalResult();
        }
    }
}
using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Constants
{
    public class Ldc_I4_2 : IOpCode
    {
        public Code Code => Code.Ldc_I4_2;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(new I4Value(2));
            return new NormalResult();
        }
    }
}
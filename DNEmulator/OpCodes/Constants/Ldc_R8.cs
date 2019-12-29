using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Constants
{
    public class Ldc_R8 : IOpCode
    {
        public Code Code => Code.Ldc_R8;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(new R8Value((double)ctx.Instruction.Operand));
            return new NormalResult();
        }
    }
}
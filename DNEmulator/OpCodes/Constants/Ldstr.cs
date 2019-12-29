using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Constants
{
    public class Ldstr : IOpCode
    {
        public Code Code => Code.Ldstr;

        public EmulationResult Emulate(Context ctx)
        {
            ctx.Stack.Push(new StringValue((string)ctx.Instruction.Operand));
            return new NormalResult();
        }
    }
}
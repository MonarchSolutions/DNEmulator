using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Linq;

namespace DNEmulator.OpCodes.Invocation
{
    public class Jmp : IOpCode
    {
        public Code Code => Code.Jmp;

        public EmulationResult Emulate(Context ctx)
        {
            var method = ((IMethod)ctx.Instruction.Operand).ResolveMethodDef();
            var emulator = new Emulator(method, ctx.Stack.Pop(method.Parameters.Count).Reverse());
            ctx.Stack.Clear();
            emulator.Emulate();
            return new ReturnResult();
        }
    }
}

using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Linq;

namespace DNEmulator.OpCodes.Invocation
{
    public class Jmp : OpCodeEmulator
    {
        public override Code Code => Code.Jmp;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Instruction.Operand is IMethod iMethod))
                throw new InvalidILException(ctx.Instruction.ToString());

            var method = iMethod is MethodDef methodDef ? methodDef : iMethod.ResolveMethodDef();
            var emulator = new CILEmulator(method, ctx.Stack.Pop(method.Parameters.Count).Reverse());
            ctx.Stack.Clear();
            emulator.Emulate();
            return new ReturnResult();
        }
    }
}

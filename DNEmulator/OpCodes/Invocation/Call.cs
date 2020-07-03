using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Linq;

namespace DNEmulator.OpCodes.Invocation
{
    public class Call : OpCodeEmulator
    {
        public override Code Code => Code.Call;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Instruction.Operand is IMethod iMethod))
                throw new InvalidILException(ctx.Instruction.ToString());

            var method = (iMethod is MethodDef methodDef) ? methodDef : iMethod.ResolveMethodDef();
            var emulator = new CILEmulator(method, ctx.Stack.Pop(method.Parameters.Count).Reverse());
            emulator.Emulate();

            if (method.ReturnType.ElementType != ElementType.Void)
                ctx.Stack.Push(emulator.ValueStack.Pop());

            return new NormalResult();
        }
    }
}

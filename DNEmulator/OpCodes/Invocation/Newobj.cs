using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Linq;
using System.Reflection;

namespace DNEmulator.OpCodes.Invocation
{
    public class Newobj : OpCodeEmulator
    {
        public override Code Code => Code.Newobj;
        public override EmulationRequirements Requirements => EmulationRequirements.MemberExecution;

        public override EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Instruction.Operand is IMethod iMethod))
                throw new InvalidILException(ctx.Instruction.ToString());

            var constructorInfo = ctx.Emulator.DynamicContext.LookupMember<ConstructorInfo>(iMethod.MDToken.ToInt32());
            ctx.Stack.Push(new ObjectValue(constructorInfo.Invoke(ctx.Stack.PopObjects(constructorInfo.GetParameters().Length).Reverse().ToArray())));
            return new NormalResult();
        }
    }
}

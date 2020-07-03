using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Linq;
using System.Reflection;

namespace DNEmulator.OpCodes.Invocation
{
    public class Callvirt : OpCodeEmulator
    {
        public override Code Code => Code.Callvirt;
        public override EmulationRequirements Requirements => EmulationRequirements.MemberExecution;

        public override EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Instruction.Operand is IMethod iMethod))
                throw new InvalidILException(ctx.Instruction.ToString());

            var methodInfo = ctx.Emulator.DynamicContext.LookupMember<MethodInfo>(iMethod.MDToken.ToInt32());
            var parameters = ctx.Stack.PopObjects(methodInfo.GetParameters().Length).Reverse().ToArray();

            if (!(ctx.Stack.Pop() is ObjectValue objectValue))
                throw new InvalidStackException();

            object thisPtr = objectValue.Value;
            var returnValue = methodInfo.Invoke(thisPtr, parameters);
            if (methodInfo.ReturnType != typeof(void))
                ctx.Stack.Push(Value.FromObject(returnValue));
            return new NormalResult();
        }
    }
}

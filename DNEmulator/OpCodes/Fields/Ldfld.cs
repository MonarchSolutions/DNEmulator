using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Reflection;

namespace DNEmulator.OpCodes.Fields
{
    public class Ldfld : OpCodeEmulator
    {
        public override Code Code => Code.Ldfld;
        public override EmulationRequirements Requirements => EmulationRequirements.MemberExecution;

        public override EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Stack.Pop() is ObjectValue objectValue))
                throw new InvalidStackException();

            if (!(ctx.Instruction.Operand is IField field))
                throw new InvalidILException(ctx.Instruction.ToString());

            var fieldInfo = ctx.Emulator.DynamicContext.LookupMember<FieldInfo>(field.MDToken.ToInt32());
            var thisPtr = objectValue.Value;
            var fieldValue = fieldInfo.GetValue(thisPtr);
            ctx.Stack.Push(Value.FromObject(fieldValue));

            return new NormalResult();
        }
    }
}

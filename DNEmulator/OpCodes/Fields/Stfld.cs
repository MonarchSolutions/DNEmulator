using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Reflection;

namespace DNEmulator.OpCodes.Fields
{
    public class Stfld : OpCodeEmulator
    {
        public override Code Code => Code.Stfld;
        public override EmulationRequirements Requirements => EmulationRequirements.MemberExecution;

        public override EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Instruction.Operand is IField field))
                throw new InvalidILException(ctx.Instruction.ToString());

            var fieldInfo = ctx.Emulator.DynamicContext.LookupMember<FieldInfo>(field.MDToken.ToInt32());
            var newFieldValue = ctx.Stack.Pop().GetValue();
            if (!(ctx.Stack.Pop() is ObjectValue objectValue))
                throw new InvalidStackException();

            var thisPtr = objectValue.Value;
            fieldInfo.SetValue(thisPtr, newFieldValue);
            return new NormalResult();
        }
    }
}

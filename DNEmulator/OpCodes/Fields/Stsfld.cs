using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Fields
{
    public class Stsfld : IOpCode
    {
        public Code Code => Code.Stsfld;

        public EmulationResult Emulate(Context ctx)
        {
            var firstValue = ctx.Stack.Pop();

            var field = (IField)ctx.Instruction.Operand;
            if (!field.ResolveFieldDef().IsStatic)
                throw new InvalidILException(ctx.Instruction.ToString());

            ctx.Emulator.FieldMap.Set(field, firstValue);
            return new NormalResult();
        }
    }
}

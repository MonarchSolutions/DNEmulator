using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Fields
{
    public class Stsfld : OpCodeEmulator
    {
        public override Code Code => Code.Stsfld;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            var firstValue = ctx.Stack.Pop();

            if (!(ctx.Instruction.Operand is IField field))
                throw new InvalidILException(ctx.Instruction.ToString());

            if (!field.ResolveFieldDef().IsStatic)
                throw new InvalidILException(ctx.Instruction.ToString());

            ctx.Emulator.FieldMap.Set(field, firstValue);
            return new NormalResult();
        }
    }
}

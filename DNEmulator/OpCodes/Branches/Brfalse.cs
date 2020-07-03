using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Branches
{
    public class Brfalse : OpCodeEmulator
    {
        public override Code Code => Code.Brfalse;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            var firstValue = ctx.Stack.Pop();

            bool jump;
            switch (firstValue.ValueType)
            {
                case DNValueType.Int32:
                    jump = ((I4Value)firstValue).Value == 0;
                    break;
                case DNValueType.Object:
                    jump = ((ObjectValue)firstValue).Value == null;
                    break;
                default:
                    throw new InvalidStackException();
            }

            if (jump)
                return new JumpResult(ctx.Emulator.Method.Body.Instructions.IndexOf((Instruction)ctx.Instruction.Operand));

            return new NormalResult();
        }
    }
}

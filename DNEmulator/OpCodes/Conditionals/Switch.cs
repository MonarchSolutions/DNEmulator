using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Conditionals
{
    public class Switch : OpCodeEmulator
    {
        public override Code Code => Code.Switch;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Stack.Pop() is I4Value i4Value))
                throw new InvalidStackException();

            if (!(ctx.Instruction.Operand is Instruction[] instructions))
                throw new InvalidILException(ctx.Instruction.ToString());

            if (i4Value.Value < 0 || i4Value.Value > instructions.Length)
                return new NormalResult();

            var target = instructions[i4Value.Value];
            return new JumpResult(ctx.Emulator.Method.Body.Instructions.IndexOf(target));
        }
    }
}

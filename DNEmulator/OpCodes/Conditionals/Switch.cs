using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Conditionals
{
    public class Switch : IOpCode
    {
        public Code Code => Code.Switch;

        public EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Stack.Pop() is I4Value i4Value))
                throw new InvalidILException(ctx.Instruction.ToString());
            var instructions = (Instruction[])ctx.Instruction.Operand;

            if (i4Value.Value < 0 || i4Value.Value > instructions.Length)
                return new NormalResult();
    
            var target = instructions[i4Value.Value];
            return new JumpResult(ctx.Emulator.Method.Body.Instructions.IndexOf(target));
        }
    }
}

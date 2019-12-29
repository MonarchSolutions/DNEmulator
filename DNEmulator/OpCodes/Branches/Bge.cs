using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Branches
{
    public class Bge : IOpCode
    {
        public Code Code => Code.Bge;

        public EmulationResult Emulate(Context ctx)
        {
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            if (firstValue.ValueType != secondValue.ValueType)
                throw new InvalidILException(ctx.Instruction.ToString());

            var jump = false;
            switch (firstValue.ValueType)
            {
                case DNValueType.Int32:
                    jump = ((I4Value)firstValue).Value >= ((I4Value)secondValue).Value;
                    break;
                case DNValueType.Int64:
                    jump = ((I8Value)firstValue).Value >= ((I8Value)secondValue).Value;
                    break;
                case DNValueType.Real:
                    jump = ((R8Value)firstValue).Value >= ((R8Value)secondValue).Value;
                    break;
                default:
                    throw new InvalidILException(ctx.Instruction.ToString());
            }

            if (jump)
                return new JumpResult(ctx.Emulator.Method.Body.Instructions.IndexOf((Instruction)ctx.Instruction.Operand));

            return new NormalResult();
        }
    }
}

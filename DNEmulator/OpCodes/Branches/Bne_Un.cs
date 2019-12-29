using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Branches
{
    public class Bne_Un : IOpCode
    {
        public Code Code => Code.Bne_Un;

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
                    jump = ((I4Value)firstValue).Value != ((I4Value)secondValue).Value;
                    break;
                case DNValueType.Int64:
                    jump = ((I8Value)firstValue).Value != ((I8Value)secondValue).Value;
                    break;
                case DNValueType.Real:
                    jump = ((R8Value)firstValue).Value != ((R8Value)secondValue).Value;
                    break;
                case DNValueType.String:
                    jump = ((StringValue)firstValue).Value != ((StringValue)secondValue).Value;
                    break;
                case DNValueType.Object:
                    jump = ((ObjectValue)firstValue).Value != ((ObjectValue)secondValue).Value;
                    break;
                case DNValueType.Native:
                    jump = ((NativeValue)firstValue).Value != ((NativeValue)secondValue).Value;
                    break;
            }

            if (jump)
                return new JumpResult(ctx.Emulator.Method.Body.Instructions.IndexOf((Instruction)ctx.Instruction.Operand));

            return new NormalResult();
        }
    }
}

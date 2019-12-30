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

            bool jump;
            switch (firstValue.ValueType)
            {
                case DNValueType.Int32 when secondValue.ValueType == DNValueType.Int32:
                    jump = ((I4Value)firstValue).Value != ((I4Value)secondValue).Value;
                    break;
                case DNValueType.Int32 when secondValue.ValueType == DNValueType.Native:
                    jump = ((I4Value)firstValue).Value != (long)((NativeValue)secondValue).Value;
                    break;
                case DNValueType.Int64 when secondValue.ValueType == DNValueType.Int64:
                    jump = ((I8Value)firstValue).Value != ((I8Value)secondValue).Value;
                    break;
                case DNValueType.Real when secondValue.ValueType == DNValueType.Real:
                    jump = ((R8Value)firstValue).Value != ((R8Value)secondValue).Value;
                    break;
                case DNValueType.String when secondValue.ValueType == DNValueType.String:
                    jump = ((StringValue)firstValue).Value != ((StringValue)secondValue).Value;
                    break;
                case DNValueType.Object when secondValue.ValueType == DNValueType.Object:
                    jump = ((ObjectValue)firstValue).Value != ((ObjectValue)secondValue).Value;
                    break;
                case DNValueType.Native when secondValue.ValueType == DNValueType.Native:
                    jump = ((NativeValue)firstValue).Value != ((NativeValue)secondValue).Value;
                    break;
                case DNValueType.Native when secondValue.ValueType == DNValueType.Int32:
                    jump = (long)((NativeValue)firstValue).Value != ((I4Value)secondValue).Value;
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

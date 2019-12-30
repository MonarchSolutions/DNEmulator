using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Branches
{
    public class Ble : IOpCode
    {
        public Code Code => Code.Ble;

        public EmulationResult Emulate(Context ctx)
        {
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            bool jump;
            switch (firstValue.ValueType)
            {
                case DNValueType.Int32 when secondValue.ValueType == DNValueType.Int32:
                    jump = ((I4Value)firstValue).Value <= ((I4Value)secondValue).Value;
                    break;
                case DNValueType.Int32 when secondValue.ValueType == DNValueType.Native:
                    jump = ((I4Value)firstValue).Value <= (long)((NativeValue)secondValue).Value;
                    break;
                case DNValueType.Int64 when secondValue.ValueType == DNValueType.Int64:
                    jump = ((I8Value)firstValue).Value <= ((I8Value)secondValue).Value;
                    break;
                case DNValueType.Real when secondValue.ValueType == DNValueType.Real:
                    jump = ((R8Value)firstValue).Value <= ((R8Value)secondValue).Value;
                    break;
                case DNValueType.Native when secondValue.ValueType == DNValueType.Native:
                    jump = (long)((NativeValue)firstValue).Value <= (long)((NativeValue)secondValue).Value;
                    break;
                case DNValueType.Native when secondValue.ValueType == DNValueType.Int32:
                    jump = (long)((NativeValue)firstValue).Value <= ((I4Value)secondValue).Value;
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

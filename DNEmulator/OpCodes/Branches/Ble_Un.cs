using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Branches
{
    public class Ble_Un : IOpCode
    {
        public Code Code => Code.Ble_Un;

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
                    jump = (uint)((I4Value)firstValue).Value <= (uint)((I4Value)secondValue).Value;
                    break;
                case DNValueType.Int64:
                    jump = (ulong)((I8Value)firstValue).Value <= (ulong)((I8Value)secondValue).Value;
                    break;
                case DNValueType.Real:
                    //unordered
                    jump = ((R8Value)firstValue).Value <= ((R8Value)secondValue).Value;
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

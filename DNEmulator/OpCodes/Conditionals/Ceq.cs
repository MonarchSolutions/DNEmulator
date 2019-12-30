using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Conditionals
{
    public class Ceq : IOpCode
    {
        public Code Code => Code.Ceq;

        public EmulationResult Emulate(Context ctx)
        {
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            bool statement;
            switch (firstValue.ValueType)
            {
                case DNValueType.Int32 when secondValue.ValueType == DNValueType.Int32:
                    statement = ((I4Value)firstValue).Value == ((I4Value)secondValue).Value;
                    break;
                case DNValueType.Int32 when secondValue.ValueType == DNValueType.Native:
                    statement = ((I4Value)firstValue).Value == (long)((NativeValue)secondValue).Value;
                    break;
                case DNValueType.Int64 when secondValue.ValueType == DNValueType.Int64:
                    statement = ((I8Value)firstValue).Value == ((I8Value)secondValue).Value;
                    break;
                case DNValueType.Real when secondValue.ValueType == DNValueType.Real:
                    statement = ((R8Value)firstValue).Value == ((R8Value)secondValue).Value;
                    break;
                case DNValueType.String when secondValue.ValueType == DNValueType.String:
                    statement = ((StringValue)firstValue).Value == ((StringValue)secondValue).Value;
                    break;
                case DNValueType.Object when secondValue.ValueType == DNValueType.Object:
                    statement = ((ObjectValue)firstValue).Value == ((ObjectValue)secondValue).Value;
                    break;
                case DNValueType.Native when secondValue.ValueType == DNValueType.Native:
                    statement = ((NativeValue)firstValue).Value == ((NativeValue)secondValue).Value;
                    break;
                case DNValueType.Native when secondValue.ValueType == DNValueType.Int32:
                    statement = (long)((NativeValue)firstValue).Value == ((I4Value)secondValue).Value;
                    break;
                default:
                    throw new InvalidILException(ctx.Instruction.ToString());
            }
            ctx.Stack.Push((statement) ? new I4Value(1) : new I4Value(0));
            return new NormalResult();
        }
    }
}

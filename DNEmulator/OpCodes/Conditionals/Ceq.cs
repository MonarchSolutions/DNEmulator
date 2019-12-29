using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
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

            var statement = false;
            switch (firstValue.ValueType)
            {
                case DNValueType.Int32:
                    statement = ((I4Value)firstValue).Value == ((I4Value)secondValue).Value;
                    break;
                case DNValueType.Int64:
                    statement = ((I8Value)firstValue).Value == ((I8Value)secondValue).Value;
                    break;
                case DNValueType.Real:
                    statement = ((R8Value)firstValue).Value == ((R8Value)secondValue).Value;
                    break;
                case DNValueType.String:
                    statement = ((StringValue)firstValue).Value == ((StringValue)secondValue).Value;
                    break;
                case DNValueType.Object:
                    statement = ((ObjectValue)firstValue).Value == ((ObjectValue)secondValue).Value;
                    break;
                case DNValueType.Native:
                    statement = ((NativeValue)firstValue).Value == ((NativeValue)secondValue).Value;
                    break;
            }
            ctx.Stack.Push((statement) ? new I4Value(1) : new I4Value(0));
            return new NormalResult();
        }
    }
}

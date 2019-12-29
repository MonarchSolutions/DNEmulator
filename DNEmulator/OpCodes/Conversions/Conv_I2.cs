using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;


namespace DNEmulator.OpCodes.Conversions
{
    public class Conv_I2 : IOpCode
    {
        public Code Code => Code.Conv_I2;

        public EmulationResult Emulate(Context ctx)
        {
            var firstValue = ctx.Stack.Pop();

            switch (firstValue.ValueType)
            {
                case DNValueType.Int32:
                    ctx.Stack.Push(new I4Value((short)((I4Value)firstValue).Value));
                    break;
                case DNValueType.Int64:
                    ctx.Stack.Push(new I4Value((short)((I8Value)firstValue).Value));
                    break;
                case DNValueType.Real:
                    ctx.Stack.Push(new I4Value((short)((R8Value)firstValue).Value));
                    break;
                default:
                    throw new InvalidILException(ctx.Instruction.ToString());
            }
            return new NormalResult();
        }
    }
}

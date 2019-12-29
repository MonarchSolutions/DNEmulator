using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Pointer
{
    public class Stind_I1 : IOpCode
    {
        public Code Code => Code.Stind_I1;

        public unsafe EmulationResult Emulate(Context ctx)
        {
            var value = ctx.Stack.Pop();
            var address = ctx.Stack.Pop();

            if (!(value is I4Value i4Value && address is NativeValue nativeValue))
                throw new InvalidILException(ctx.Instruction.ToString());

            *(sbyte*)nativeValue.Value = (sbyte)i4Value.Value;

            return new NormalResult();
        }
    }
}

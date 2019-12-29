using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Pointer
{
    public class Stind_I8 : IOpCode
    {
        public Code Code => Code.Stind_I8;

        public unsafe EmulationResult Emulate(Context ctx)
        {
            var value = ctx.Stack.Pop();
            var address = ctx.Stack.Pop();

            if (!(value is I8Value i8Value && address is NativeValue nativeValue))
                throw new InvalidILException(ctx.Instruction.ToString());

            *(long*)nativeValue.Value = i8Value.Value;

            return new NormalResult();
        }
    }
}

using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Pointer
{
    public class Stind_R8 : OpCodeEmulator
    {
        public override Code Code => Code.Stind_R8;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override unsafe EmulationResult Emulate(Context ctx)
        {
            var value = ctx.Stack.Pop();
            var address = ctx.Stack.Pop();

            if (!(value is R8Value r8Value && address is NativeValue nativeValue))
                throw new InvalidStackException();

            *(double*)nativeValue.Value = r8Value.Value;

            return new NormalResult();
        }
    }
}

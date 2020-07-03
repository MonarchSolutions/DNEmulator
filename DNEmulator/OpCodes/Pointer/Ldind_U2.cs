using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Pointer
{
    public class Ldind_U2 : OpCodeEmulator
    {
        public override Code Code => Code.Ldind_U2;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override unsafe EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Stack.Pop() is NativeValue nativeValue))
                throw new InvalidStackException();

            ctx.Stack.Push(new I4Value(*(ushort*)nativeValue.Value));

            return new NormalResult();
        }
    }
}

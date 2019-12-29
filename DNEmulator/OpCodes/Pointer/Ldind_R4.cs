using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System.Runtime.InteropServices;

namespace DNEmulator.OpCodes.Pointer
{
    public class Ldind_R4 : IOpCode
    {
        public Code Code => Code.Ldind_R4;

        public unsafe EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Stack.Pop() is NativeValue nativeValue))
                throw new InvalidILException(ctx.Instruction.ToString());

            ctx.Stack.Push(new R8Value(*(float*)nativeValue.Value));
            return new NormalResult();
        }
    }
}

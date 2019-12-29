using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Pointer
{
    public class Ldind_I : IOpCode
    {
        public Code Code => Code.Ldind_I;

        public unsafe EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Stack.Pop() is NativeValue nativeValue))
                throw new InvalidILException(ctx.Instruction.ToString());

            ctx.Stack.Push(new NativeValue((IntPtr)nativeValue.Value));

            return new NormalResult();
        }
    }
}

﻿using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Pointer
{
    public class Ldind_I : OpCodeEmulator
    {
        public override Code Code => Code.Ldind_I;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override unsafe EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Stack.Pop() is NativeValue nativeValue))
                throw new InvalidStackException();

            ctx.Stack.Push(new NativeValue(nativeValue.Value));

            return new NormalResult();
        }
    }
}

using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;


namespace DNEmulator.OpCodes.Misc
{
    public class Ckfinite : OpCodeEmulator
    {
        public override Code Code => Code.Ckfinite;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Stack.Pop() is R8Value r8Value))
                throw new InvalidStackException();

            if (double.IsNaN(r8Value.Value) || double.IsInfinity(r8Value.Value))
                throw new ArithmeticException();

            return new NormalResult();
        }

    }
}

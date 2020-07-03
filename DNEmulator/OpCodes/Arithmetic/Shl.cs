using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Arithmetic
{
    public class Shl : OpCodeEmulator
    {
        public override Code Code => Code.Shl;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            if (!(secondValue is I4Value bitAmount))
                throw new InvalidStackException();


            switch (firstValue.ValueType)
            {
                case DNValueType.Int32:
                    ctx.Stack.Push(new I4Value(((I4Value)firstValue).Value << bitAmount.Value));
                    break;
                case DNValueType.Int64:
                    ctx.Stack.Push(new I8Value(((I8Value)firstValue).Value << bitAmount.Value));
                    break;
                default:
                    throw new InvalidStackException();
            }

            return new NormalResult();
        }
    }
}

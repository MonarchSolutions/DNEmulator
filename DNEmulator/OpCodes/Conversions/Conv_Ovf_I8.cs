﻿using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Conversions
{
    public class Conv_Ovf_I8 : OpCodeEmulator
    {
        public override Code Code => Code.Conv_Ovf_I8;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            var firstValue = ctx.Stack.Pop();

            checked
            {
                switch (firstValue.ValueType)
                {
                    case DNValueType.Int32:
                        ctx.Stack.Push(new I8Value(((I4Value)firstValue).Value));
                        break;
                    case DNValueType.Int64:
                        ctx.Stack.Push(new I8Value(((I8Value)firstValue).Value));
                        break;
                    case DNValueType.Real:
                        ctx.Stack.Push(new I8Value((long)((R8Value)firstValue).Value));
                        break;
                    default:
                        throw new InvalidStackException();
                }
            }

            return new NormalResult();
        }
    }
}

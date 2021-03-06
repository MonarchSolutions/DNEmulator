﻿using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Conditionals
{
    public class Cgt_Un : OpCodeEmulator
    {
        public override Code Code => Code.Cgt_Un;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            bool statement;
            switch (firstValue.ValueType)
            {
                case DNValueType.Int32 when secondValue.ValueType == DNValueType.Int32:
                    statement = (uint)((I4Value)firstValue).Value > (uint)((I4Value)secondValue).Value;
                    break;
                case DNValueType.Int32 when secondValue.ValueType == DNValueType.Native:
                    statement = (uint)((I4Value)firstValue).Value > (ulong)((NativeValue)secondValue).Value;
                    break;
                case DNValueType.Int64 when secondValue.ValueType == DNValueType.Int64:
                    statement = (ulong)((I8Value)firstValue).Value > (ulong)((I8Value)secondValue).Value;
                    break;
                case DNValueType.Real when secondValue.ValueType == DNValueType.Real:
                    //unordered
                    statement = ((R8Value)firstValue).Value > ((R8Value)secondValue).Value;
                    break;
                case DNValueType.Native when secondValue.ValueType == DNValueType.Native:
                    statement = (ulong)((NativeValue)firstValue).Value > (ulong)((NativeValue)secondValue).Value;
                    break;
                case DNValueType.Native when secondValue.ValueType == DNValueType.Int32:
                    statement = (ulong)((NativeValue)firstValue).Value > (uint)((I4Value)secondValue).Value;
                    break;
                default:
                    throw new InvalidStackException();
            }
            ctx.Stack.Push((statement) ? new I4Value(1) : new I4Value(0));
            return new NormalResult();
        }
    }
}

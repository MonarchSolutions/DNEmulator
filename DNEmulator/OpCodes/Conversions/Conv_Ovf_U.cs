using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;

using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Conversions
{
    public class Conv_Ovf_U : OpCodeEmulator
    {
        public override Code Code => Code.Conv_Ovf_U;
        public override EmulationRequirements Requirements => EmulationRequirements.None;

        public override EmulationResult Emulate(Context ctx)
        {
            var firstValue = ctx.Stack.Pop();

            checked
            {
                switch (firstValue.ValueType)
                {
                    case DNValueType.Int32:
                        ctx.Stack.Push(new NativeValue((IntPtr)(long)(ulong)(UIntPtr)((I4Value)firstValue).Value));
                        break;
                    case DNValueType.Int64:
                        ctx.Stack.Push(new NativeValue((IntPtr)(long)(ulong)(UIntPtr)((I8Value)firstValue).Value));
                        break;
                    case DNValueType.Real:
                        ctx.Stack.Push(new NativeValue((IntPtr)(long)(ulong)(UIntPtr)((R8Value)firstValue).Value));
                        break;
                    default:
                        throw new InvalidStackException();
                }
            }

            return new NormalResult();
        }
    }
}

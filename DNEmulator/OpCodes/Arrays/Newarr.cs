using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;

namespace DNEmulator.OpCodes.Arrays
{
    public class Newarr : IOpCode
    {
        public Code Code => Code.Newarr;

        public EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Stack.Pop() is I4Value count))
                throw new InvalidILException(ctx.Instruction.ToString());

            switch(((ITypeDefOrRef)ctx.Instruction.Operand).ToTypeSig().ElementType)
            {
                case ElementType.I:
                    ctx.Stack.Push(new ObjectValue(new IntPtr[count.Value]));
                    break;
                case ElementType.U:
                    ctx.Stack.Push(new ObjectValue(new UIntPtr[count.Value]));
                    break;
                case ElementType.I1:
                    ctx.Stack.Push(new ObjectValue(new sbyte[count.Value]));
                    break;
                case ElementType.U1:
                    ctx.Stack.Push(new ObjectValue(new byte[count.Value]));
                    break;
                case ElementType.I2:
                    ctx.Stack.Push(new ObjectValue(new short[count.Value]));
                    break;
                case ElementType.U2:
                    ctx.Stack.Push(new ObjectValue(new ushort[count.Value]));
                    break;
                case ElementType.I4:
                    ctx.Stack.Push(new ObjectValue(new int[count.Value]));
                    break;
                case ElementType.U4:
                    ctx.Stack.Push(new ObjectValue(new uint[count.Value]));
                    break;
                case ElementType.I8:
                    ctx.Stack.Push(new ObjectValue(new long[count.Value]));
                    break;
                case ElementType.U8:
                    ctx.Stack.Push(new ObjectValue(new ulong[count.Value]));
                    break;
                case ElementType.R4:
                    ctx.Stack.Push(new ObjectValue(new float[count.Value]));
                    break;
                case ElementType.R8:
                    ctx.Stack.Push(new ObjectValue(new double[count.Value]));
                    break;
                case ElementType.String:
                    ctx.Stack.Push(new ObjectValue(new string[count.Value]));
                    break;
                case ElementType.Char:
                    ctx.Stack.Push(new ObjectValue(new char[count.Value]));
                    break;
                case ElementType.Boolean:
                    ctx.Stack.Push(new ObjectValue(new bool[count.Value]));
                    break;
                case ElementType.Object:
                    ctx.Stack.Push(new ObjectValue(new object[count.Value]));
                    break;
            }

            return new NormalResult();
        }
    }
}

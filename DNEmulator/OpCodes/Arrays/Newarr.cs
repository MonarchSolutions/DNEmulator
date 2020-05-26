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

            if (!(ctx.Instruction.Operand is ITypeDefOrRef typeDefOrRef))
                throw new InvalidILException(ctx.Instruction.ToString());

            ctx.Stack.Push(new ObjectValue(GetArray(typeDefOrRef.ToTypeSig(), count.Value)));
           

            return new NormalResult();
        }


        private static Array GetArray(TypeSig typeSignature, int count)
        {
            switch(typeSignature.ElementType)
            {
                case ElementType.I:
                    return new IntPtr[count];
                case ElementType.U:
                    return new UIntPtr[count];
                case ElementType.I1:
                    return new sbyte[count];
                case ElementType.U1:
                    return new byte[count];
                case ElementType.I2:
                    return new short[count];
                case ElementType.U2:
                    return new ushort[count];
                case ElementType.I4:
                    return new int[count];
                case ElementType.U4:
                    return new uint[count];
                case ElementType.I8:
                    return new long[count];
                case ElementType.U8:
                    return new ulong[count];
                case ElementType.R4:
                    return new float[count];
                case ElementType.R8:
                    return new double[count];
                case ElementType.String:
                    return new string[count];
                case ElementType.Char:
                    return new char[count];
                case ElementType.Boolean:
                    return new bool[count];
                case ElementType.Object:
                    return new object[count];
                case ElementType.SZArray:
                case ElementType.Array:
                    var type = Type.GetType(typeSignature.FullName + "[]");
                    if (type == null)
                        return null;
                    return Array.CreateInstance(type, count);
            }

            return null;
        }
    }
}

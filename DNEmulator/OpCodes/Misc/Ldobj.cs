using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Runtime.InteropServices;

namespace DNEmulator.OpCodes.Misc
{
    public class Ldobj : IOpCode
    {
        public Code Code => Code.Ldobj;

        public EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Stack.Pop() is NativeValue address))
                throw new InvalidILException(ctx.Instruction.ToString());

            if (!(ctx.Instruction.Operand is ITypeDefOrRef typeDefOrRef))
                throw new InvalidILException(ctx.Instruction.ToString());

            var type = Type.GetType(typeDefOrRef.FullName);
            if (type == null)
            {
                ctx.Stack.Push(new ObjectValue(null));
                return new NormalResult();
            }

            ctx.Stack.Push(new ObjectValue(Marshal.PtrToStructure(address.Value, type)));
            return new NormalResult();
        }
    }
}

using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using SRE = System.Reflection.Emit;

namespace DNEmulator.OpCodes.Misc
{
    public class Sizeof : OpCodeEmulator
    {
        public override Code Code => Code.Sizeof;
        public override EmulationRequirements Requirements => EmulationRequirements.MemberLoading;

        private readonly IDictionary<Type, int> _typeSizeCache = new Dictionary<Type, int>();
        public override EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Instruction.Operand is IType iType))
                throw new InvalidILException(ctx.Instruction.ToString());

            var type = ctx.Emulator.DynamicContext.LookupMember<Type>(iType.MDToken.ToInt32());
            if (!_typeSizeCache.TryGetValue(type, out int size))
            {
                var dynamicMethod = new DynamicMethod(string.Empty, typeof(int), Type.EmptyTypes);
                var ilGenerator = dynamicMethod.GetILGenerator();
                ilGenerator.Emit(SRE.OpCodes.Sizeof, type);
                ilGenerator.Emit(SRE.OpCodes.Ret);
                size = (int)dynamicMethod.Invoke(null, new object[0]);
                _typeSizeCache.Add(type, size);
            }

            ctx.Stack.Push(new I4Value(size));
            return new NormalResult();
        }
    }
}

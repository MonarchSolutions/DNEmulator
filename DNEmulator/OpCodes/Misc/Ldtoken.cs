using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Reflection;

namespace DNEmulator.OpCodes.Misc
{
    public class Ldtoken : OpCodeEmulator
    {
        public override Code Code => Code.Ldtoken;
        public override EmulationRequirements Requirements => EmulationRequirements.MemberLoading;

        public override EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Instruction.Operand is IDnlibDef iDnlibDef))
                throw new InvalidILException(ctx.Instruction.ToString());
            var member = ctx.Emulator.DynamicContext.LookupMember<MemberInfo>(iDnlibDef.MDToken.ToInt32());
            ctx.Stack.Push(new ObjectValue(member));
            return new NormalResult();
        }
    }
}

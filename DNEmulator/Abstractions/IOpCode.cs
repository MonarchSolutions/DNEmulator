using dnlib.DotNet.Emit;

namespace DNEmulator.Abstractions
{
    public abstract class OpCodeEmulator
    {
        public abstract EmulationResult Emulate(Context ctx);
        public abstract Code Code
        {
            get;
        }
        public abstract EmulationRequirements Requirements
        {
            get;
        }

        public bool IsDynamic
        {
            get
            {
                return Requirements.HasFlag(EmulationRequirements.MemberLoading);
            }
        }

        public bool IsStatic
        {
            get
            {
                return !IsDynamic;
            }
        }
    }
}

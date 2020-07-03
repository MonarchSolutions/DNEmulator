using System;

namespace DNEmulator
{
    [Flags]
    public enum EmulationRequirements
    {
        None = 1 << 0,
        MemberLoading = 1 << 1,
        MemberExecution = MemberLoading | (1 << 2),
    }
}

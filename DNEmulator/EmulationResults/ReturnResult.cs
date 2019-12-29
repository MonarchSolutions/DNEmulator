using DNEmulator.Abstractions;
using DNEmulator.Enumerations;

namespace DNEmulator.EmulationResults
{
    public class ReturnResult : EmulationResult
    {
        public override EmulationState State => EmulationState.Ret;
    }
}

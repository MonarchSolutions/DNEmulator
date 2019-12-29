using DNEmulator.Abstractions;
using DNEmulator.Enumerations;

namespace DNEmulator.EmulationResults
{
    public class NormalResult : EmulationResult
    {
        public override EmulationState State => EmulationState.Next;
    }
}

using DNEmulator.Abstractions;
using DNEmulator.Enumerations;

namespace DNEmulator.EmulationResults
{
    public class JumpResult : EmulationResult
    {
        public override EmulationState State => EmulationState.Jump;

        public int Index { get; }
        public JumpResult(int index)
        {
            Index = index;
        }
    }
}

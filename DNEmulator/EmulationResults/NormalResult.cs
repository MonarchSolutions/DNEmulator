using DNEmulator.Abstractions;


namespace DNEmulator.EmulationResults
{
    public class NormalResult : EmulationResult
    {
        public override EmulationState State => EmulationState.Next;
    }
}

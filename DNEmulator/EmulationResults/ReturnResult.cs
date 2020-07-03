using DNEmulator.Abstractions;


namespace DNEmulator.EmulationResults
{
    public class ReturnResult : EmulationResult
    {
        public override EmulationState State => EmulationState.Ret;
    }
}

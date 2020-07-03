using DNEmulator.Abstractions;


namespace DNEmulator.EmulationResults
{
    public class JumpResult : EmulationResult
    {
        public override EmulationState State => EmulationState.Jump;

        public JumpResult(int index)
        {
            Index = index;
        }
        public int Index
        {
            get;
        }

    }
}

using DNEmulator.Enumerations;

namespace DNEmulator
{
    public abstract class EmulationResult
    {
        public abstract EmulationState State { get; }
    }
}

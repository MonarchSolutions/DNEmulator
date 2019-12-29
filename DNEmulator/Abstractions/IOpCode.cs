using DNEmulator.Enumerations;
using dnlib.DotNet.Emit;

namespace DNEmulator.Abstractions
{
    public interface IOpCode
    {
        Code Code { get; }
        EmulationResult Emulate(Context ctx);
    }
}

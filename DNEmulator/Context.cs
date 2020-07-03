using dnlib.DotNet.Emit;

namespace DNEmulator
{
    public class Context
    {
        public Context(CILEmulator emulator)
        {
            Emulator = emulator;
            Stack = emulator.ValueStack;
        }

        public ValueStack Stack
        {
            get;
        }
        public CILEmulator Emulator
        {
            get;
        }

        public Instruction Instruction
        {
            get;
            set;
        }
    }
}

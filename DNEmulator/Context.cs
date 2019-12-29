using DNEmulator.Abstractions;
using dnlib.DotNet.Emit;
using System.Collections.Generic;

namespace DNEmulator
{
    public class Context
    {
        public ValueStack Stack { get; }
        public Emulator Emulator { get; }
        public Instruction Instruction { get; set; }

        public Context(Emulator emulator)
        {
            Emulator = emulator;
            Stack = emulator.ValueStack;
        }
    }
}

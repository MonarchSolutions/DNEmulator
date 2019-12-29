using System;

namespace DNEmulator.Exceptions
{
    public class InvalidILException : Exception
    {
        public InvalidILException(string instr) : base("Invalid IL found: " + instr.ToString())
        {

        }
    }
}

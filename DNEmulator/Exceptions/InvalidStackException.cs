using System;

namespace DNEmulator.Exceptions
{
    public class InvalidStackException : Exception
    {
        public InvalidStackException() : base("Invalid Stack found.")
        {

        }
    }
}

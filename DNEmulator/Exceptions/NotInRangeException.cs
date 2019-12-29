using System;

namespace DNEmulator.Exceptions
{
    public class NotInRangeException : Exception
    {
        public NotInRangeException(string name) : base(name + " was not in range!")
        {

        }
    }
}

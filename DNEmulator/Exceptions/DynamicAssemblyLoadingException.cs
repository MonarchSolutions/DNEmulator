using System;

namespace DNEmulator.Exceptions
{
    public class DynamicAssemblyLoadingException : Exception
    {
        public DynamicAssemblyLoadingException(Exception ex) : base($"Failed to load Assembly. {ex}")
        {
            //
        }
    }
}

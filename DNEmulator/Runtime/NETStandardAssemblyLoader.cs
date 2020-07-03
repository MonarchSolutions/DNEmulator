
using DNEmulator.Abstractions;
using System.Reflection;

namespace DNEmulator.Runtime
{
    public class NETStandardAssemblyLoader : IAssemblyLoader
    {
        public Assembly Load(byte[] rawAssembly)
        {
            return Assembly.Load(rawAssembly);
        }

        public Assembly Load(string assemblyPath)
        {
            return Assembly.LoadFile(assemblyPath);
        }
    }
}


using System.Reflection;

namespace DNEmulator.Abstractions
{
    public interface IAssemblyLoader
    {
        Assembly Load(byte[] rawAssembly);
        Assembly Load(string assemblyPath);
    }
}

using DNEmulator.Dynamic.NETCore;
using dnlib.DotNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Internal;

namespace DNEmulator.Tests
{
    [TestClass]
    public class AssemblyLoadingTest
    {

        [TestMethod]
        public void Load1()
        {
            var assembly = typeof(AssemblyLoadingTest).Assembly;
            var dynamicContext = new DynamicContext(assembly.Location, new NETCoreAssemblyLoader());
            Assert.IsNotNull(dynamicContext.Assembly);
        }

        [TestMethod]
        public void Load2()
        {
            var assembly = typeof(AssemblyLoadingTest).Assembly;
            var dynamicContext = new DynamicContext(assembly.Location, new NETCoreAssemblyLoader(), true);
            Assert.IsNotNull(dynamicContext.Assembly);
            Assert.IsTrue(dynamicContext.AllowInvocation);
        }
    }
}

using DNEmulator.Values;
using dnlib.DotNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DNEmulator.Tests.Invocation
{
    [TestClass()]
    public class CallvirtTest
    {
        [TestMethod()]
        public void ToStringTest()
        {
            var assembly = typeof(CallvirtTest).Assembly;
            var module = ModuleDefMD.Load(assembly.Location);
            var dynamicContext = new DynamicContext(assembly)
            {
                AllowInvocation = true,
            };
            var cilEmulator = new CILEmulator(module.FindNormal("DNEmulator.Tests.Invocation.CallvirtTest").FindMethod("TestMethod"),
                              dynamicContext);
            cilEmulator.Emulate();
            var value = cilEmulator.ValueStack.Pop();
            Assert.AreEqual(typeof(ObjectValue), value.GetType());
            var emulatedToString = (ObjectValue)value;
            var toString = TestMethod();
            Assert.AreEqual(toString, (string)emulatedToString.Value);
        }

        private string TestMethod()
        {
            string helloWorld = "Hello World";
            return helloWorld.Substring(0, 5);
        }
      
    }
}

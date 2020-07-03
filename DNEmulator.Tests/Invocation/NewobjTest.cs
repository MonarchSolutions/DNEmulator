using DNEmulator.Values;
using dnlib.DotNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DNEmulator.Tests.Invocation
{
    [TestClass()]
    public class NewobjTest
    {
        [TestMethod()]
        public void DateTimeTest()
        {
            var assembly = typeof(CallvirtTest).Assembly;
            var module = ModuleDefMD.Load(assembly.Location);
            var dynamicContext = new DynamicContext(assembly)
            {
                AllowInvocation = true,
            };
            var cilEmulator = new CILEmulator(module.FindNormal("DNEmulator.Tests.Invocation.NewobjTest").FindMethod("TestMethod"),
                              dynamicContext);
            cilEmulator.Emulate();
            var value = cilEmulator.ValueStack.Pop();
            Assert.AreEqual(typeof(ObjectValue), value.GetType());
            var emulatedDateTime = (ObjectValue)value;
            var dateTime = TestMethod();
            Assert.AreEqual(dateTime, (DateTime)emulatedDateTime.Value);
        }

        private DateTime TestMethod()
        {
            return new DateTime(1879, 3, 14);
        }

    }
}

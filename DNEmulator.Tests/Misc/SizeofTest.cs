using DNEmulator.Values;
using dnlib.DotNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;

namespace DNEmulator.Tests.Misc
{
    [TestClass()]
    public class SizeofTest
    {
        struct TestStruct1
        {
            double pX;
            double pY;
            byte pos;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct TestStruct2
        {
            int x;
            int y;
            int z;
            TestStruct1 testStruct;
        }

        [TestMethod()]
        public void CalculateSize()
        {
            var assembly = typeof(SizeofTest).Assembly;
            var module = ModuleDefMD.Load(assembly.Location);
            var dynamicContext = new DynamicContext(assembly);

            var cilEmulator = new CILEmulator(module.FindNormal("DNEmulator.Tests.Misc.SizeofTest").FindMethod("TestMethod"),
                              dynamicContext);
            cilEmulator.Emulate();
            var value = cilEmulator.ValueStack.Pop();
            Assert.AreEqual(typeof(I4Value), value.GetType());
            var emulatedSize = (I4Value)value;
            int size = TestMethod();
            Assert.AreEqual(size, emulatedSize.Value);
        }

        private unsafe int TestMethod()
        {
            return sizeof(TestStruct1) + sizeof(TestStruct2);
        }
    }
}

using DNEmulator.Values;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace DNEmulator.Tests
{
    class Program
    {

        private static Emulator _emulator;
        static void Main(string[] args)
        {

            var module = ModuleDefMD.Load(Assembly.GetEntryAssembly().Location);
            Resolve(module);
            _emulator = new Emulator(module.FindNormal("DNEmulator.Tests.Program").FindMethod("ToEmulate"));
           // _emulator.BeforeEmulation += BeforeEmulation;
           // _emulator.BeforeEmulation += OwnHandler;
           // _emulator.AfterEmulation += AfterEmulation;
            _emulator.Emulate();
            
            if(_emulator.ValueStack.Pop() is I4Value result)
                Console.WriteLine("Emulated Result: " + result.Value);

            Console.WriteLine("Normal-Call Result: " + ToEmulate());
            Console.ReadKey();
        }

      
        private static void BeforeEmulation(Instruction instruction)
        {
            Console.WriteLine("Emulating: " + instruction.ToString());
        }

        private static void AfterEmulation(Instruction instruction)
        {
            Console.WriteLine("Emulated: " + instruction.ToString() + "!");
        }

        private static void OwnHandler(Instruction instruction)
        {
            if (instruction.OpCode.Code != Code.Call)
                return;

            var method = (IMethod)instruction.Operand;
            if (!(method.Name == "Abs" && method.MethodSig.Params.Count == 1 && method.MethodSig.Params[0].ElementType == ElementType.R8))
                return;

            if (!(_emulator.ValueStack.Pop() is R8Value r8Value))
                return;

            _emulator.ValueStack.Push(new R8Value(Math.Abs(r8Value.Value)));

            //Tell the emulator not to emulate the call since its handled now:
            _emulator.Continue();

        }

        private static void Resolve(ModuleDefMD module)
        {
            var asmResolver = new AssemblyResolver();
            var modCtx = new ModuleContext(asmResolver);
            asmResolver.DefaultModuleContext = modCtx;
            module.Context = modCtx;
            asmResolver.EnableTypeDefCache = true;
            var resolver = (AssemblyResolver)module.Context.AssemblyResolver;
            foreach (var asmRef in module.GetAssemblyRefs())
            {
                var def = resolver.Resolve(asmRef, module);
                resolver.AddToCache(def);
            }
        }

        static int ToEmulate()
        {
           var evenNumbers = new int[500];
           var counter = 0;
           for(var i = 0; i < 1000; i++)
           {
                if (i % 2 == 0)
                    evenNumbers[counter++] = i;
           }
            return Add(evenNumbers);
        }

        static int Add(int[] numbers)
        {
            var result = 0;
            for (var i = 0; i < numbers.Length; i++)
                result += numbers[i];

            return result;
        }
    }
}

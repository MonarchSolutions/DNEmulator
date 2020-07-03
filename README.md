# DNEmulator [WIP]
A stack emulator for the Common Intermediate Language, which follows the ECMA Standards (based on dnlib)

# Usage
Important namespaces:
```C#
  using DNEmulator;
  using DNEmulator.Values;
  using DNEmulator.EmulationResults;
```

Creating a new instance of the emulator:
```C#
  var module = ModuleDefMD.Load(Assembly.GetEntryAssembly().Location);
  var emulator = new CILEmulator(module.FindNormal("DNEmulator.Tests.Program").FindMethod("ToEmulate"));   
```

Creating a new instance of the emulator with parameter values:
```C#
  var module = ModuleDefMD.Load(Assembly.GetEntryAssembly().Location);
  var emulator = new CILEmulator(module.FindNormal("DNEmulator.Tests.Program").FindMethod("ToEmulate"), new Value[] { new ObjectValue("abc"), new ObjectValue(new int[5]) });   
```

By using the instantiations given above, the emulator will not support dynamic opcodes. Dynamic opcodes are opcodes which require loading of runtime assemblies or execution of parts of them. To support dynamic opcodes we need to create a new dynamic context and pass it as parameter to "CILEmulator":
```C#
  System.Reflection.Assembly assembly = ...;
  var dynamicContext = new DynamicContext(assembly);
  var emulator = new CILEmulator(..., dynamicContext);
```

Or create a dynamic context by using a loader:
```C#
  IAssemblyLoader assemblyLoader = ...;
  var dynamicContext = new DynamicContext(assemblyLoader);
```

Load the Assembly by using the constructor of "DynamicContext" or by using the "Load" Functions:
```C#
  DynamicContext dynamicContext = ...;
  byte[] assemblyBytes = ...;
  dynamicContext.Load(assemblyBytes);
  string assemblyPath = ...;
  dynamicContext.Load(assemblyPath);
```

It will allow invocation by default. However you can disallow invocation by setting the flag in the constructor or by setting the "AllowInvocation" flag to "false"
```C#
  DynamicContext dynamicContext = ...;
  dynamicContext.AllowInvocation = false;
```
  

Emulating Method:
```C#
  var module = ModuleDefMD.Load(Assembly.GetEntryAssembly().Location);
  var emulator = new CILEmulator(module.FindNormal("DNEmulator.Tests.Program").FindMethod("ToEmulate")); 
  emulator.Emulate();
```

Emulating Instruction:
```C#
  var module = ModuleDefMD.Load(Assembly.GetEntryAssembly().Location);
  var method = module.FindNormal("DNEmulator.Tests.Program").FindMethod("ToEmulate");
  var emulator = new CILEmulator(method); 
  foreach(var instruction in method.Body.Instructions)
  {
     var result = emulator.EmulateInstruction(instruction);
  }
```

Accessing Stack:
```C#
  var value = emulator.ValueStack.Pop();
  var values = emulator.ValueStack.Pop(3);
  emulator.ValueStack.Push(new I4Value(0));
```

Accessing Locals:
```C#
  var value = emulator.LocalMap.Get(method.Body.Variables[0]);
  emulator.LocalMap.Set(method.Body.Variables[0], new I4Value(1));
```

Accessing Parameters:
```C#
  var value = emulator.ParameterMap.Get(method.Parameters[0]);
  emulator.LocalMap.Set(method.Parameters[0]), new I4Value(1));
```

Accessing Fields:
```C#
  IField field = ...;
  var value = emulator.FieldMap.Get(field);
  emulator.FieldMap.Set(field), new R8Value(1.0));
```

Events:
```C#
  static void Main(string[] args)
  {
     var module = ModuleDefMD.Load(Assembly.GetEntryAssembly().Location);
     var emulator = new CILEmulator(module.FindNormal("DNEmulator.Tests.Program").FindMethod("ToEmulate"));
     emulator.BeforeEmulation += BeforeEmulation;
     emulator.AfterEmulation += AfterEmulation;        
  }
  
  private static void BeforeEmulation(Instruction instruction)
  {
    Console.WriteLine("Emulating: " + instruction.ToString());
  }

  private static void AfterEmulation(Instruction instruction)
  {
     Console.WriteLine("Emulated: " + instruction.ToString() + "!");
  }
```

# Values
```
  DNEmulator.Values.I4Value -> int32 value (I4)
  DNEmulator.Values.I8Value -> int64 value (I8)
  DNEmulator.Values.NativeValue -> native int value (I)
  DNEmulator.Values.ObjectValue -> object value (O)
  DNEmulator.Values.R8Value -> float(32/64) value (F)
  DNEmulator.Values.UnknownValue -> unknown value
```

# Emulation Results
```
  DNEmulator.EmulationResults.NormalResult -> emulation will continue
  DNEmulator.EmulationResults.JumpResult -> emulator will jump to instruction at given index
  DNEmulator.EmulationResults.ReturnResult -> emulation will end
```

# Dynamic Loaders
```
  DNEmulator.Dynamic.NETCore.NETCoreAssemblyLoader -> is able to load every .NET Core Assembly
  DNEmulator.Dynamic.NETCore.NETFrameworkAssemblyLoader -> is able to load every .NET Framework Assembly
  DNEmulator.Abstractions.IAssemblyLoader -> provides methods that every assembly loader should have and makes it possible to create own custom assembly loaders
```

# Discord
https://discord.me/nre

# Credits
0xd4d - dnlib 



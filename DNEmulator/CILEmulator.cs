using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Maps;
using DNEmulator.Values;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;

namespace DNEmulator
{
    public class CILEmulator
    {
        private readonly Context _context;
        private int _index;

        private static readonly IDictionary<Code, OpCodeEmulator> _opCodeMap = new Dictionary<Code, OpCodeEmulator>();
        static CILEmulator()
        {
            foreach (var type in typeof(CILEmulator).Module.GetTypes())
            {
                if (type.IsAbstract)
                    continue;

                if (!typeof(OpCodeEmulator).IsAssignableFrom(type))
                    continue;

                var opCode = (OpCodeEmulator)Activator.CreateInstance(type);
                _opCodeMap.Add(opCode.Code, opCode);
            }
        }
        public CILEmulator(MethodDef method, DynamicContext dynamicContext = null, IEnumerable<Value> parameterValues = null)
        {
            Method = method;

            if (!Method.HasBody)
                throw new EmulationException($"Cannot create Emulator instance for method without body.");

            if (!Method.IsIL)
                throw new NotSupportedException();

            _context = new Context(this);
            DynamicContext = dynamicContext;

            ParameterMap = new ParameterMap(method.Parameters, parameterValues);
            LocalMap = new LocalMap(method.Body.Variables);
        }

        public CILEmulator(MethodDef method, IEnumerable<Value> parameterValues) : this(method, null, parameterValues)
        {

        }



        public delegate void EmulationEventHandler(Instruction instruction);
        public event EmulationEventHandler BeforeEmulation;
        public event EmulationEventHandler AfterEmulation;


        public ValueStack ValueStack
        {
            get;
        } = new ValueStack();
        public MethodDef Method
        {
            get;
        }

        public DynamicContext DynamicContext
        {
            get;
            set;
        }

        public Map LocalMap
        {
            get;
        }
        public Map ParameterMap
        {
            get;
        }

        public FieldMap FieldMap
        {
            get;
        } = new FieldMap();


        public void Emulate()
        {
            var state = EmulationState.Next;
            do
            {
                var instruction = Method.Body.Instructions[_index++];
                BeforeEmulation?.Invoke(instruction);
                var result = EmulateInstruction(instruction);
                AfterEmulation?.Invoke(instruction);
                state = result.State;
                if (state == EmulationState.Jump)
                    _index = ((JumpResult)result).Index;

            } while (state != EmulationState.Ret);
        }


        public EmulationResult EmulateInstruction(Instruction instruction)
        {
            var code = Simplify(instruction.OpCode.Code);
            if (!(_opCodeMap.TryGetValue(code, out var opCode) && IsEmulationAllowed(opCode)))
            {
                EmulateStackBehaviour(instruction);
                return new NormalResult();
            }
            _context.Instruction = instruction;
            return opCode.Emulate(_context);
        }

        private bool IsEmulationAllowed(OpCodeEmulator opCodeEmulator)
        {
            if (opCodeEmulator.IsStatic)
                return true;
            if (DynamicContext is null)
                return false;
            if (!DynamicContext.AllowInvocation && opCodeEmulator.Requirements.HasFlag(EmulationRequirements.MemberExecution))
                return false;

            return true;
        }

        private void EmulateStackBehaviour(Instruction instruction)
        {
            instruction.CalculateStackUsage(out var pushes, out var pops);

            for (int i = 0; i < pops; i++)
                _context.Stack.Pop();
            for (int i = 0; i < pushes; i++)
                _context.Stack.Push(new UnknownValue(default));
        }

        public void Continue() => _index++;

        private static Code Simplify(Code code) => code switch
        {
            Code.Beq_S => Code.Beq,
            Code.Bge_S => Code.Bge,
            Code.Bge_Un_S => Code.Bge_Un,
            Code.Bgt_S => Code.Bgt,
            Code.Bgt_Un_S => Code.Bgt_Un,
            Code.Ble_S => Code.Ble,
            Code.Ble_Un_S => Code.Ble_Un,
            Code.Blt_S => Code.Blt,
            Code.Blt_Un_S => Code.Blt_Un,
            Code.Bne_Un_S => Code.Bne_Un,
            Code.Br_S => Code.Br,
            Code.Brfalse_S => Code.Brfalse,
            Code.Brtrue_S => Code.Brtrue,
            Code.Ldarg_S => Code.Ldarg,
            Code.Ldarga_S => Code.Ldarga,
            Code.Ldloca_S => Code.Ldloca,
            Code.Leave_S => Code.Leave,
            Code.Stloc_S => Code.Stloc,
            Code.Ldloc_S => Code.Ldloc,
            _ => code,
        };

    }
}

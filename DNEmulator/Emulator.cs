using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
using DNEmulator.Exceptions;
using DNEmulator.Maps;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;

namespace DNEmulator
{
    public class Emulator
    {

        private static readonly IDictionary<Code, IOpCode> _opCodeMap = new Dictionary<Code, IOpCode>();
        static Emulator()
        {
            foreach(var type in typeof(Emulator).Module.GetTypes())
            {
                if (type == typeof(IOpCode))
                    continue;

                if (!typeof(IOpCode).IsAssignableFrom(type))
                    continue;

                var opCode = (IOpCode)Activator.CreateInstance(type);
                _opCodeMap.Add(opCode.Code, opCode);
            }
        }
        public ValueStack ValueStack { get; } = new ValueStack();
        public MethodDef Method { get; }

        public Map LocalMap { get; }
        public Map ParameterMap { get; }

        public FieldMap FieldMap { get; } = new FieldMap();

        private Context _context;

        public Emulator(MethodDef method, IEnumerable<Value> parameterValues = null)
        {
            Method = method;
            if (!Method.HasBody)
                throw new EmulationException($"Cannot create Emulator instance for method without body.");

            if (!Method.IsIL)
                throw new NotSupportedException();

            _context = new Context(this);

            ParameterMap = new ParameterMap(method.Parameters, parameterValues);
            LocalMap = new LocalMap(method.Body.Variables);
        }

        public delegate void EmulationEventHandler(Instruction instruction);
        public event EmulationEventHandler BeforeEmulation;
        public event EmulationEventHandler AfterEmulation;

        private int _index;
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
            var opCode = _opCodeMap[code];
            _context.Instruction = instruction;
            return opCode.Emulate(_context);
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

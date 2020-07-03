using DNEmulator.Abstractions;
using DNEmulator.Values;
using dnlib.DotNet;
using System.Collections.Generic;

namespace DNEmulator.Maps
{
    public class FieldMap
    {
        private readonly IDictionary<IField, Value> _values = new Dictionary<IField, Value>();

        public Value Get(IField field)
        {
            if (!_values.TryGetValue(field, out var value))
                _values.Add(field, new UnknownValue(field.FieldSig.Type.ElementType));

            return value;
        }


        public void Set(IField field, Value value)
        {
            if (!_values.ContainsKey(field))
            {
                _values.Add(field, value);
                return;
            }
            _values[field] = value;
        }

    }
}

using DNEmulator.Abstractions;
using DNEmulator.Values;
using dnlib.DotNet;
using System.Collections.Generic;

namespace DNEmulator.Maps
{
    public class FieldMap
    {
        private IDictionary<IField, Value> _values = new Dictionary<IField, Value>();

        public Value Get(IField field)
        {
            if (!_values.ContainsKey(field))
                _values.Add(field, new UnknownValue(field.FieldSig.Type.ElementType));

            return _values[field];
        }


        public void Set(IField field, Value value)
        {
            if(!_values.ContainsKey(field))
            {
                _values.Add(field, value);
                return;
            }
            _values[field] = value;
        }

    }
}

using DNEmulator.Exceptions;
using dnlib.DotNet;
using System.Collections.Generic;

namespace DNEmulator.Abstractions
{
    public abstract class Map
    {
        protected IDictionary<IVariable, Value> _values = new Dictionary<IVariable, Value>();

        public Value Get(IVariable variable)
        {
            if (!_values.ContainsKey(variable))
                throw new NotInRangeException(variable.ToString());

            return _values[variable];
        }

        public void Set(IVariable variable, Value value)
        {
            if (!_values.ContainsKey(variable))
                throw new NotInRangeException(variable.ToString());

            _values[variable] = value;
        }
    }
}

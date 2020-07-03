using DNEmulator.Abstractions;
using DNEmulator.Values;
using dnlib.DotNet;
using System.Collections.Generic;
using System.Linq;

namespace DNEmulator.Maps
{
    public class ParameterMap : Map
    {
        public ParameterMap(ParameterList parameters, IEnumerable<Value> parameterValues)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                var parameter = parameters[i];
                _values.Add(parameter, parameterValues == null ? new UnknownValue(parameter.Type.ElementType) : parameterValues.ElementAt(i));
            }
        }
    }
}

using DNEmulator.Abstractions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.Maps
{
    public class LocalMap : Map
    {
        public LocalMap(LocalList locals)
        {
            for (int i = 0; i < locals.Count; i++)
            {
                var variable = locals[i];
                _values.Add(variable, new UnknownValue(variable.Type.ElementType));
            }
        }
    }
}

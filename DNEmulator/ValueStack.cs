using DNEmulator.Abstractions;
using System;
using System.Collections.Generic;

namespace DNEmulator
{
    public class ValueStack : Stack<Value>
    {
        public Value[] Pop(int amount)
        {
            var values = new Value[amount];
            for (var i = 0; i < amount; i++)
                values[i] = Pop();

            return values;
        }
    }
}

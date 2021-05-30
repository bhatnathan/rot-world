using UnityEngine;
using System;

[Serializable]
public class FloatReference : GenericVariableReference<float>
{
    public static implicit operator float(FloatReference reference)
    {
        return reference.Value;
    }
}

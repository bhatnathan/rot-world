using UnityEngine;
using System;

[Serializable]
public class QuaternionReference : GenericVariableReference<Quaternion>
{
    public static implicit operator Quaternion(QuaternionReference reference)
    {
        return reference.Value;
    }
}

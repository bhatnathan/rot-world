using UnityEngine;
using System;

[Serializable]
public class Vector3Reference : GenericVariableReference<Vector3>
{
    public static implicit operator Vector3(Vector3Reference reference)
    {
        return reference.Value;
    }
}

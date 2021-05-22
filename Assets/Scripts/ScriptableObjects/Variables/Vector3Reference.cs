using UnityEngine;
using System;

[Serializable]
public class Vector3Reference
{
    [SerializeField] private bool useConstant = true;
    [SerializeField] private Vector3 constantValue;
    [SerializeField] private Vector3Variable variable;

    public Vector3Reference()
    { }

    public Vector3Reference(Vector3 value)
    {
        useConstant = true;
        constantValue = value;
    }

    public Vector3 Value
    {
        get { return useConstant ? constantValue : variable.Value; }
    }

    public static implicit operator Vector3(Vector3Reference reference)
    {
        return reference.Value;
    }
}

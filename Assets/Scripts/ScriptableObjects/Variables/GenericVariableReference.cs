using UnityEngine;
using System;

[Serializable]
public abstract class GenericVariableReference<T>
{
    [SerializeField] private bool useConstant = true;
    [SerializeField] private T constantValue;
    [SerializeField] private GenericVariable<T> variable;

    public GenericVariableReference()
    { }

    public GenericVariableReference(T value)
    {
        useConstant = true;
        constantValue = value;
    }

    public T Value
    {
        get { return useConstant ? constantValue : variable.Value; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericList<T> : ScriptableObject
{
    private List<T> values = new List<T>();

    public void Add(T value)
    {
        if (!values.Contains(value))
            values.Add(value);
    }

    public void Remove(T value)
    {
        if (values.Contains(value))
            values.Remove(value);
    }

    public List<T> Values
    {
        get { return values; }
    }
}
using UnityEngine;

public abstract class GenericVariable<T> : ScriptableObject
{
    [SerializeField] private T value;

    public void SetValue(T value)
    {
        this.value = value;
    }

    public void SetValue(GenericVariable<T> value)
    {
        this.value = value.value;
    }

    public T Value
    {
        get { return value; }
    }
}

using UnityEngine;

[CreateAssetMenu]
public class Vector3Variable : ScriptableObject
{
    private Vector3 value;

    public void SetValue(Vector3 value)
    {
        this.value = value;
    }

    public void SetValue(Vector3Variable value)
    {
        this.value = value.value;
    }

    public Vector3 Value
    {
        get { return value; }
    }
}

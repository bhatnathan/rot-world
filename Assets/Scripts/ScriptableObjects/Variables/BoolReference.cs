using System;

[Serializable]
public class BoolReference : GenericVariableReference<bool>
{
    public static implicit operator bool(BoolReference reference)
    {
        return reference.Value;
    }
}

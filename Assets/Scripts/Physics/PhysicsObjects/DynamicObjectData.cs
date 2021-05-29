using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectData
{
    private Vector3 lastSafePosition;
    private bool isGrounded;

    public DynamicObjectData()
    {
        lastSafePosition = Vector3.zero;
        isGrounded = false;
    }

    public void SetSafePosition(Vector3 safe_pos)
    {
        lastSafePosition = safe_pos;
    }

    public void SetGrounded(bool is_grounded)
    {
        isGrounded = is_grounded;
    }

    public Vector3 LastSafePosition()
    {
        return lastSafePosition;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

}

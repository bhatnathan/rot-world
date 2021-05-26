using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WorldRotator))]
public class WorldRotationInputListener : MonoBehaviour
{
    private WorldRotator worldRotator;

    private void Awake()
    {
        worldRotator = GetComponent<WorldRotator>();
    }

    public void OnRotateAroundX(InputAction.CallbackContext context)
    {
        if (IsInputPerformed(context))
        {
            InvokeWorldRotation(Axis.X, Rotation.Clockwise);
        }
    }

    public void OnRotateAroundXCounterclockwise(InputAction.CallbackContext context)
    {
        if (IsInputPerformed(context))
        {
            InvokeWorldRotation(Axis.X, Rotation.Counterclockwise);
        }
    }

    public void OnRotateAroundY(InputAction.CallbackContext context)
    {
        if (IsInputPerformed(context))
        {
            InvokeWorldRotation(Axis.Y, Rotation.Clockwise);
        }
    }

    public void OnRotateAroundYCounterclockwise(InputAction.CallbackContext context)
    {
        if (IsInputPerformed(context))
        {
            InvokeWorldRotation(Axis.Y, Rotation.Counterclockwise);
        }
    }

    public void OnRotateAroundZ(InputAction.CallbackContext context)
    {
        if (IsInputPerformed(context))
        {
            InvokeWorldRotation(Axis.Z, Rotation.Clockwise);
        }
    }

    public void OnRotateAroundZCounterclockwise(InputAction.CallbackContext context)
    {
        if (IsInputPerformed(context))
        {
            InvokeWorldRotation(Axis.Z, Rotation.Counterclockwise);
        }
    }

    private void InvokeWorldRotation(Axis axis, Rotation rotation_direction)
    {       
        worldRotator.RotateDesiredCamera(axis, rotation_direction);
    }
    
    private bool IsInputPerformed(InputAction.CallbackContext context)
    {
        return context.performed;
    }

}

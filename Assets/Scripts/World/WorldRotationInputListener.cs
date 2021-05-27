using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WorldRotator))]
public class WorldRotationInputListener : MonoBehaviour
{
    private WorldRotator worldRotator;
    
    private Rotation rotationDirection;

    private void Awake()
    {
        worldRotator = GetComponent<WorldRotator>();
    }

    public void OnRotationModifier(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rotationDirection = Rotation.Counterclockwise;
        }
        else if(context.canceled)
        {
            rotationDirection = Rotation.Clockwise;
        }
    }

    public void OnRotateAroundX(InputAction.CallbackContext context)
    {
        if (IsInputPerformed(context))
        {            
            InvokeWorldRotation(Axis.X, rotationDirection);
        }
    }


    public void OnRotateAroundY(InputAction.CallbackContext context)
    {
        if (IsInputPerformed(context))
        {
            InvokeWorldRotation(Axis.Y, rotationDirection);
        }
    }

    public void OnRotateAroundZ(InputAction.CallbackContext context)
    {
        if (IsInputPerformed(context))
        {
            InvokeWorldRotation(Axis.Z, rotationDirection);
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

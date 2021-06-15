using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WorldRotator))]
public class WorldRotationInputListener : MonoBehaviour
{
    [Tooltip("Reference to Is Time Stopped")]
    [SerializeField] private BoolReference isTimeStopped;
    [SerializeField] private Vector2ToRotationConfig rotationInputMapping;    

    private WorldRotator worldRotator;
    private Direction rotationDirection;

    private void Awake()
    {
        worldRotator = GetComponent<WorldRotator>();
    }

    public void OnRotationModifier(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rotationDirection = Direction.Counterclockwise;
        }
        else if(context.canceled)
        {
            rotationDirection = Direction.Clockwise;
        }
    }

    public void OnRotateAroundX(InputAction.CallbackContext context)
    {
        if (IsInputPerformed(context))
        {            
            InvokeWorldRotation(new Rotation(Axis.X, rotationDirection));
        }
    }


    public void OnRotateAroundY(InputAction.CallbackContext context)
    {
        if (IsInputPerformed(context))
        {
            InvokeWorldRotation(new Rotation(Axis.Y, ~rotationDirection & Direction.Counterclockwise));
        }
    }

    public void OnRotateAroundZ(InputAction.CallbackContext context)
    {
        if (IsInputPerformed(context))
        {
            InvokeWorldRotation(new Rotation(Axis.Z, ~rotationDirection & Direction.Counterclockwise));
        }
    }

    public void OnRotate(Vector2 input_value)
    {
        Rotation rotation = rotationInputMapping.Vector2ToRotation(input_value);
        InvokeWorldRotation(rotation);
    }

    private void InvokeWorldRotation(Rotation rotation)
    {
        if (isTimeStopped.Value)
        {
            worldRotator.RotateWorld(rotation);
        }
    }
    
    private bool IsInputPerformed(InputAction.CallbackContext context)
    {
        return context.performed;
    }

}

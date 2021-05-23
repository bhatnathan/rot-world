using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private float rotationTime; //How long approx it should take to complete a rotation
    [SerializeField] private QuaternionVariable worldRotation;

    private Quaternion initialRotation;
    private Quaternion latestSafeRotation;

    private float rotationVelocity;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = latestSafeRotation = transform.rotation;
        worldRotation.SetValue(initialRotation);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRotation();
    }

    //TODO: Create a custom Input Composite type to streamline rotational input, after input has been decided.
    public void OnRotateHorizontal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float value = context.ReadValue<float>();

            Vector3 input_direction;
            Rotation rotation_direction;
            if (value > 0)
            {
                input_direction = Vector3.up;
                rotation_direction = Rotation.Clockwise;
            }
            else if (value < 0)
            {
                input_direction = Vector3.up;
                rotation_direction = Rotation.Counterclockwise;
            }
            else
            {
                return;
            }

            RotateDesiredCamera(input_direction, rotation_direction);
        }
    }

    public void OnRotateVerticalRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float value = context.ReadValue<float>();

            Vector3 input_direction;
            Rotation rotation_direction;
            if (value > 0)
            {
                input_direction = Vector3.forward;
                rotation_direction = Rotation.Clockwise;
            }
            else if (value < 0)
            {
                input_direction = Vector3.forward;
                rotation_direction = Rotation.Counterclockwise;
            }
            else
            {
                return;
            }

            RotateDesiredCamera(input_direction, rotation_direction);
        }
    }

    public void OnRotateVerticalLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float value = context.ReadValue<float>();

            Vector3 input_direction;
            Rotation rotation_direction;
            if (value > 0)
            {
                input_direction = Vector3.right;
                rotation_direction = Rotation.Clockwise;
            }
            else if (value < 0)
            {
                input_direction = Vector3.right;
                rotation_direction = Rotation.Counterclockwise;
            }
            else
            {
                return;
            }

            RotateDesiredCamera(input_direction, rotation_direction);
        }
    }

    void RotateDesiredCamera(Vector3 axis, Rotation direction)
    {       
        worldRotation.SetValue(Quaternion.AngleAxis(direction.Equals(Rotation.Clockwise) ? 90 : -90, worldRotation.Value * axis)* worldRotation.Value);
    }

    void UpdateCameraRotation()
    {
        //Slerp towards desired rotation
        float angle_diff = Quaternion.Angle(transform.rotation, worldRotation.Value);
        float new_angle_diff = Mathf.SmoothDamp(angle_diff, 0, ref rotationVelocity, rotationTime);

        float t = angle_diff > Mathf.Epsilon ? 1.0f - new_angle_diff / angle_diff : 1.0f;

        transform.rotation = Quaternion.Slerp(transform.rotation, worldRotation.Value, t);
    }
}

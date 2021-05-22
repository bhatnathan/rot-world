using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //BEGIN TEMP USING THIS FOR DEBUG INPUT
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateDesiredCamera(Vector3.right, true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotateDesiredCamera(Vector3.right, false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            RotateDesiredCamera(Vector3.up, true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RotateDesiredCamera(Vector3.up, false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RotateDesiredCamera(Vector3.forward, true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RotateDesiredCamera(Vector3.forward, false);
        }
        //END TEMP

        UpdateCameraRotation();
    }

    void RotateDesiredCamera(Vector3 axis, bool clockwise)
    {
        worldRotation.SetValue(Quaternion.AngleAxis(clockwise ? 90 : -90, worldRotation.Value * axis) * worldRotation.Value);
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

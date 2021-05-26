using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private float rotationTime; //How long approx it should take to complete a rotation    
    [SerializeField] private QuaternionReference worldRotationReference;

    private Quaternion initialRotation;
    private Quaternion latestSafeRotation;

    private float rotationVelocity;

    // Start is called before the first frame update
    void Start()
    {        
        transform.rotation = worldRotationReference.Value;
        initialRotation = latestSafeRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRotation();
    }
    

    void UpdateCameraRotation()
    {
        //Slerp towards desired rotation
        float angle_diff = Quaternion.Angle(transform.rotation, worldRotationReference.Value);
        float new_angle_diff = Mathf.SmoothDamp(angle_diff, 0, ref rotationVelocity, rotationTime);

        float t = angle_diff > Mathf.Epsilon ? 1.0f - new_angle_diff / angle_diff : 1.0f;

        transform.rotation = Quaternion.Slerp(transform.rotation, worldRotationReference.Value, t);
    }
}

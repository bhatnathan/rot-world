using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitcher : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("Reference to the world's rotation")]
    [SerializeField] private QuaternionReference worldRotation;
    [Tooltip("Reference to the world's rotation")]
    [SerializeField] private BoolVariable isTimeStopped;

    [Header("Parameters")]
    [Tooltip("How strong is the force of gravity")]
    [SerializeField] private float gravityStrength;

    private Quaternion lastRotation;

    private void Awake()
    {
        SetGravity(transform.rotation);
    }

    private void FixedUpdate()
    {
        if (isTimeStopped.Value) { return; }

        if(worldRotation.Value != lastRotation)
        {            
            SetGravity(worldRotation.Value);
            lastRotation = worldRotation.Value;
        }
    }

    private void SetGravity(Quaternion rotation)
    {
        Physics.gravity = rotation * Vector3.down * gravityStrength;
    }    
}

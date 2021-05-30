using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitcher : MonoBehaviour
{
    [Tooltip("Reference to the world's rotation")]
    [SerializeField] private QuaternionReference worldRotation;
    [Tooltip("How strong is the force of gravity")]
    [SerializeField] private float gravityStrength;

    private Quaternion lastRotation;

    private void Awake()
    {
        SetGravity(transform.rotation);
    }

    private void FixedUpdate()
    {
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

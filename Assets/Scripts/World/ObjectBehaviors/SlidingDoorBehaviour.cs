using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorBehaviour : MonoBehaviour
{
    [Tooltip("Which direction does the door slide in")]
    [SerializeField] private Vector3 slideDir;
    [Tooltip("How far does the door slide")]
    [SerializeField] private float slideAmount;
    [Tooltip("Reference to the world's rotation")]
    [SerializeField] private QuaternionReference worldRotation;
    [Tooltip("The collObj gameobject (assumes it is a child of this)")]
    [SerializeField] private GameObject collObj;
    [Tooltip("The rendered gameobject (assumes it is a child of this)")]
    [SerializeField] private GameObject meshObj;

    private bool shifted;
    private Vector3 velocity;
    private bool animating;

    // Start is called before the first frame update
    void Start()
    {
        slideDir.Normalize();
        shifted = false;
        velocity = Vector3.zero;
        animating = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetShift();
        Animate();
    }

    private void SetShift()
    {
        Vector3 gravity_dir = worldRotation.Value * Vector3.down;
        float dot_value = Vector3.Dot(gravity_dir, slideDir);

        if (!shifted && dot_value > MathConstants.smallValue)
        {
            collObj.transform.localPosition = slideDir * slideAmount;
            shifted = true;
            animating = true;
        }
        else if (shifted && dot_value < -MathConstants.smallValue)
        {
            collObj.transform.localPosition = Vector3.zero;
            shifted = false;
            animating = true;
        }
    }

    private void Animate()
    {
        if (!animating)
            return;
        if (shifted)
        {
            velocity += slideDir * Physics.gravity.magnitude * Time.fixedDeltaTime;
            meshObj.transform.localPosition += velocity * Time.fixedDeltaTime;

            if(Vector3.Dot(meshObj.transform.localPosition, slideDir) < 0)
            {
                meshObj.transform.localPosition = Vector3.zero;
                velocity = Vector3.zero;
            }

            if(meshObj.transform.localPosition.magnitude > slideAmount)
            {
                meshObj.transform.localPosition = slideDir * slideAmount;
                velocity = Vector3.zero;
                animating = false;
            }
        }
        else
        {
            velocity += slideDir * -Physics.gravity.magnitude * Time.fixedDeltaTime;
            meshObj.transform.localPosition += velocity * Time.fixedDeltaTime;

            if (meshObj.transform.localPosition.magnitude > slideAmount)
            {
                meshObj.transform.localPosition = slideDir * slideAmount;
                velocity = Vector3.zero;
            }

            if (Vector3.Dot(meshObj.transform.localPosition, slideDir) < 0)
            {
                meshObj.transform.localPosition = Vector3.zero;
                velocity = Vector3.zero;
                animating = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + slideAmount * slideDir);
        Gizmos.DrawWireCube(transform.position + slideAmount * slideDir, meshObj.transform.localScale);
    }
}

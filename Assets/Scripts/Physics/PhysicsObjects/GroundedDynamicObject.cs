using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedDynamicObject : MonoBehaviour
{
    [Tooltip("Reference to the world's rotation.")]
    [SerializeField] private QuaternionReference worldRotation;
    [Tooltip("Layers considered to be ground.")]
    [SerializeField] private LayerMask groundLayer;
    [Tooltip("Apply to which rigidbody")]
    [SerializeField] private Rigidbody body;
    [Tooltip("Distance from edge of player to edge of ground check, prevents sticking to walls.")]
    [SerializeField] private float edgeOffset = 0.05f;

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    private float yposition;
    private bool groundHit;
    private Vector3 down;

    private Vector3[] offsets;

    private void Awake()
    {
        SetOffsets();        
    }

    private void SetOffsets()
    {
        float width = transform.localScale.x / 2f - edgeOffset;
        float depth = transform.localScale.z / 2f - edgeOffset;

        offsets = new Vector3[] {
            new Vector3(width, 0f, depth),
            new Vector3(-width, 0f, depth),
            new Vector3(width, 0f, -depth),
            new Vector3(-width, 0f, -depth)
        };
    }

    private void FixedUpdate()
    {
        down = worldRotation.Value * Vector3.down;

        if (TryGetGround(out RaycastHit hit))
        {
            yposition = hit.point.y + (transform.localScale.y / 2f);
            groundHit = true;
        } else
        {
            groundHit = false;
        }
        
        StartCoroutine(AfterFixedUpdate());        
    }

    private IEnumerator AfterFixedUpdate()
    {
        yield return waitForFixedUpdate;
                
        if(groundHit && body.position.y < yposition)
        {            
            body.position = new Vector3(body.position.x, 
                Mathf.Max(body.position.y, yposition), body.position.z);
            body.velocity = new Vector3(
                body.velocity.x,
                Mathf.Max(0f, body.velocity.y),
                body.velocity.z);
        }
    }

    private bool TryGetGround(out RaycastHit hit)
    {                
        foreach(Vector3 offset in offsets)
        {
            Ray ray = new Ray(body.position - (down * 0.5f) + offset, down);
            if (Physics.Raycast(ray, out hit, 1f, groundLayer) && hit.collider != null)
            {                
                return true;
            }
        }

        hit = new RaycastHit();
        return false;
    }

}

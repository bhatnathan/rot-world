using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(DynamicObject))]
public class GroundedDynamicObject : MonoBehaviour
{
    [Tooltip("Reference to the world's rotation.")]
    [SerializeField] private QuaternionReference worldRotation;     
    [Tooltip("Distance from edge of player to edge of ground check, prevents sticking to walls.")]
    [SerializeField] private float edgeOffset = 0.05f;

    private Rigidbody body;
    private DynamicObject dynamicObject;

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    private Vector3 savedHeight;
    private bool groundHit;

    private Vector3[] offsets;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        dynamicObject = GetComponent<DynamicObject>();        
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
        Vector3 world_down = worldRotation.Value * Vector3.down;
        Vector3 world_up = -world_down;

        if (TryGetGround(out RaycastHit hit, world_down))
        {
            savedHeight = Vector3.Dot(hit.point + world_up * (transform.localScale.y / 2f), world_up) * world_up;
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

        Vector3 world_up = worldRotation.Value * Vector3.up;
        Vector3 new_height = Vector3.Dot(body.position, world_up) * world_up;

        Vector3 height_diff = new_height - savedHeight;

        if (groundHit && Vector3.Dot(height_diff, world_up) < 0.0f)
        {            
            body.position -= height_diff;

            Vector3 downwards_velocity = -Mathf.Min(Vector3.Dot(body.velocity, world_up), 0.0f) * world_up;
            body.velocity += downwards_velocity;
        }
    }

    private bool TryGetGround(out RaycastHit hit, Vector3 world_down)
    {                
        foreach(Vector3 offset in offsets)
        {
            Ray ray = new Ray(body.position - (world_down * (transform.localScale.y / 2f)) + worldRotation.Value * offset, world_down);
            if (Physics.Raycast(ray, out hit, 1f, dynamicObject.GetGroundLayer()) && hit.collider != null)
            {                
                return true;
            }
        }

        hit = new RaycastHit();
        return false;
    }

}

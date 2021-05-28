using UnityEngine;

public class RelativeLayerMaskQuery : MonoBehaviour
{   
    [Tooltip("Width of the query relative to the parent gameObject")]
    [SerializeField] private float width;

    [Tooltip("Height of the query relative to the parent gameObject")]
    [SerializeField] private float height;

    [Tooltip("Depth of the query relative to the parent gameObject")]
    [SerializeField] private float depth;

    [Tooltip("How far out of the parent gameObject we check")]
    [SerializeField] private float delta;

    private Collider[] preallocatedCollider = new Collider[1];

    /**
	 * Returns whether or not layer is to the below, relative to rotation.
	 */
    public bool IsLayerClose(LayerMask layer, Quaternion rotation)
    {
        Vector3 floor_pos = rotation * (transform.position + Vector3.down * (height * 0.5f + delta * 0.5f));
        return Physics.OverlapBoxNonAlloc(floor_pos, new Vector3(width *0.5f, delta * 0.5f, depth * 0.5f), preallocatedCollider, rotation, layer) != 0;
    }
}

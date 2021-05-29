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
    public bool IsLayerDown(LayerMask layer, Vector3 position, Quaternion rotation)
    {
        return Physics.OverlapBoxNonAlloc(GetFloorPosition(position, rotation), new Vector3(width * 0.5f, delta * 0.5f, depth * 0.5f), preallocatedCollider, rotation, layer) != 0;
    }

    private Vector3 GetFloorPosition(Vector3 position, Quaternion rotation)
    {
        return position + (rotation * Vector3.down * (height * 0.5f + delta * 0.5f));
    }
}

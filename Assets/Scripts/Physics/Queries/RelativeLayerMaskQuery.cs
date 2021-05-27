using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeLayerMaskQuery : MonoBehaviour
{
    [SerializeField] private float width; // width of the query relative to the parent gameObject
    [SerializeField] private float height; // height of the query relative to the parent gameObject
    [SerializeField] private float depth; // depth of the query relative to the parent gameObject
    [SerializeField] private float delta; // how far out of the parent gameObject we check

    /**
	 * Returns whether or not layer is to the below, relative to roation.
	 */
    public bool IsLayerClose(LayerMask layer, Quaternion rotation)
    {
        Vector3 floor_pos = rotation * (transform.position + Vector3.down * (height * 0.5f + delta * 0.5f));
        Collider[] dummy_results = new Collider[1];
        return Physics.OverlapBoxNonAlloc(floor_pos, new Vector3(width *0.5f, delta * 0.5f, depth * 0.5f), dummy_results, rotation, layer) != 0;
    }
}

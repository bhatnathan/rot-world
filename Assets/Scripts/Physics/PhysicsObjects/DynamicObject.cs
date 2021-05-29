using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RelativeLayerMaskQuery))]
public class DynamicObject : MonoBehaviour
{
    [Tooltip("List of all the active dynamic object's datas")]
    [SerializeField] private DynamicObjectDataList dynamicObjectDatas;
    [Tooltip("Reference to the world's rotation")]
    [SerializeField] private QuaternionReference worldRotation;
    [Tooltip("What layer do we consider ground for this object.")]
    [SerializeField] private LayerMask groundLayer;

    private DynamicObjectData data = new DynamicObjectData();

    //Components
    private Rigidbody body;
    private RelativeLayerMaskQuery analyser;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        analyser = GetComponent<RelativeLayerMaskQuery>();
    }

    void OnEnable()
    {
        dynamicObjectDatas.Add(data);
    }

    void OnDisable()
    {
        dynamicObjectDatas.Remove(data);
    }

    // Update is called once per frame
    void Update()
    {
        if(analyser.IsLayerClose(groundLayer, worldRotation.Value) && Mathf.Abs(Vector3.Dot(body.velocity, worldRotation.Value * Vector3.down)) < Mathf.Epsilon)
        {
            data.SetGrounded(true);
            data.SetSafePosition(transform.position);
        }
        else
        {
            data.SetGrounded(false);
        }
    }

    public bool IsGrounded()
    {
        return data.IsGrounded();
    }

    public Vector3 LastSafePosition()
    {
        return data.LastSafePosition();
    }
}

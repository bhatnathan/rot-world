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
    [Tooltip("Where is the range limit to be considered fallen off the world.")]
    [SerializeField] private FloatReference despawnLimit;
    [Tooltip("Event to raise if the object falls out of range.")]
    [SerializeField] private GameEvent onFallOffEvent;

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
    void FixedUpdate()
    {
        SetData();
        CheckFallOff();
    }

    public bool IsGrounded()
    {
        return data.IsGrounded();
    }

    public Vector3 LastSafePosition()
    {
        return data.LastSafePosition();
    }

    public Quaternion GetWorldRotation()
    {
        return worldRotation.Value;
    }

    private void SetData()
    {
        if (analyser.IsLayerDown(groundLayer, transform.position, worldRotation.Value))
        {
            data.SetGrounded(true);
            data.SetSafePosition(transform.position);
        }
        else
        {
            data.SetGrounded(false);
        }
    }

    private void CheckFallOff()
    {
        if (-Vector3.Dot(transform.position, worldRotation.Value * Vector3.down) < despawnLimit.Value)
            onFallOffEvent.Raise();
    }
}

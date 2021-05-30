using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RelativeLayerMaskQuery))]
public class DynamicObject : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("List of all the active dynamic object's datas")]
    [SerializeField] private DynamicObjectDataList dynamicObjectDatas;
    [Tooltip("Reference to the world's rotation")]
    [SerializeField] private QuaternionReference worldRotation;
    [Tooltip("The Tag for the Stage Bounds trigger collider.")]
    [SerializeField] private string stageBoundsTag; //TODO: Replace with some sort of Tag system :)
    [Header("Parameters")]
    [Tooltip("Is this object essential to complete the level")]
    [SerializeField] private bool isEssential;
    [Tooltip("What layer do we consider ground for this object.")]
    [SerializeField] private LayerMask groundLayer;
    [Header("Events")]
    [Tooltip("Event to raise if the object falls out of range.")]
    [SerializeField] private GameEvent onFallOffEvent;

    private DynamicObjectData data;

    //Components
    private Rigidbody body;
    private RelativeLayerMaskQuery analyser;

    void Awake()
    {
        data = new DynamicObjectData(isEssential);
    }

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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(stageBoundsTag) && onFallOffEvent != null)
        {
            onFallOffEvent.Raise();
        }
    }

    public void OnEssentialFallOff()
    {
        body.position = LastSafePosition();
        body.velocity = Vector3.zero;
    }
}

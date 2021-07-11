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
    [SerializeField] private string stageBoundsTag = "StageBounds"; //TODO: Replace with some sort of Tag system :)
    [Tooltip("The Tag for the Death Ground trigger collider.")]
    [SerializeField] private string deathGroundTag = "DeathGround"; //TODO: Replace with some sort of Tag system :)
    [Header("Parameters")]
    [Tooltip("Is this object essential to complete the level")]
    [SerializeField] private bool isEssential;
    [Tooltip("What layer do we consider ground for this object.")]
    [SerializeField] private LayerMask groundLayer;
    [Tooltip("Maximum velocity of this object.")]
    [SerializeField] private float maxVelocity;
    [Header("Events")]
    [Tooltip("Event to raise if the object falls out of range.")]
    [SerializeField] private GameEvent onFallOffEvent;
    [Tooltip("Event to raise if the object goes from not grounded to grounded.")]
    [SerializeField] private GameEvent onGroundedEvent;

    private DynamicObjectData data;
    private Vector3 initialPos;

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
        initialPos = transform.position;

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
        if (body.velocity.magnitude > maxVelocity)
            body.velocity = body.velocity.normalized * maxVelocity;
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

    public LayerMask GetGroundLayer()
    {
        return groundLayer;
    }

    private void SetData()
    {
        if (analyser.IsLayerDown(groundLayer, transform.position, worldRotation.Value) 
            && Mathf.Abs(Vector3.Dot(worldRotation.Value * Vector3.down, body.velocity)) < MathConstants.smallishValue)
        {
            if(!data.IsGrounded())
            {
                if (onGroundedEvent != null)
                    onGroundedEvent.Raise();
            }

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(deathGroundTag) && onFallOffEvent != null)
        {
            onFallOffEvent.Raise();
        }
    }

    public void OnEssentialFallOff()
    {
        body.position = LastSafePosition();
        body.velocity = Vector3.zero;
    }

    public void OnLevelReset()
    {
        transform.position = initialPos;
        body.velocity = Vector3.zero;
    }
}

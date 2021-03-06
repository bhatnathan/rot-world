using UnityEngine;
using System.Linq;

public class WorldRotator : MonoBehaviour
{
    [Tooltip("Reference to the world's rotation")]
    [SerializeField] private QuaternionVariable worldRotation;
    [Tooltip("List of all the active dynamic object's datas")]
    [SerializeField] private DynamicObjectDataList dynamicObjectDatas;
    [Tooltip("Event to raise on world rotation.")]
    [SerializeField] private GameEvent onRotateEvent;
    [Tooltip("How long after a rotation do we wait before starting to check safe positions.")]
    [SerializeField] private float safeCheckDelay;

    private Quaternion initialRotation;
    private Quaternion latestSafeRotation;

    private bool potentialUnsafeRotation; //we've rotated but we don't know if it's safe yet
    private float safeCheckDelayTimer;

    private void Awake()
    {
        worldRotation.SetValue(transform.rotation);
        latestSafeRotation = initialRotation = transform.rotation;
        potentialUnsafeRotation = false;
        safeCheckDelayTimer = 0;
    }

    void FixedUpdate()
    {
        if (safeCheckDelayTimer >= safeCheckDelay)
            SetSafePosition();
        else
            safeCheckDelayTimer += Time.fixedDeltaTime;

    }

    public void RotateWorld(Rotation rotation)
    {        
        Vector3 axis_vector = AxisUtils.AxisToVector(rotation.axis);
        worldRotation.SetValue(Quaternion.AngleAxis(rotation.direction.Equals(Direction.Clockwise) ? 90 : -90, worldRotation.Value * axis_vector) * worldRotation.Value);
        potentialUnsafeRotation = true;
        safeCheckDelayTimer = 0;

        if (onRotateEvent != null)
            onRotateEvent.Raise();
    }

    private void SetSafePosition()
    {
        if(potentialUnsafeRotation && dynamicObjectDatas.Values.All(obj => !obj.IsEssential() || obj.IsGrounded()))
        {
            latestSafeRotation = worldRotation.Value;
            potentialUnsafeRotation = false;
        }
    }

    public void OnEssentialFallOff()
    {
        worldRotation.SetValue(latestSafeRotation);
    }

    public void OnLevelReset()
    {
        worldRotation.SetValue(initialRotation);
    }
}

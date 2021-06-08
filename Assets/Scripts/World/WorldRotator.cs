using UnityEngine;
using System.Linq;

public class WorldRotator : MonoBehaviour
{
    [Tooltip("Reference to the world's rotation")]
    [SerializeField] private QuaternionVariable worldRotation;
    [Tooltip("List of all the active dynamic object's datas")]
    [SerializeField] private DynamicObjectDataList dynamicObjectDatas;

    private Quaternion initialRotation;
    private Quaternion latestSafeRotation;

    private bool potentialUnsafeRotation; //we've rotated but we don't know if it's safe yet

    private void Awake()
    {
        worldRotation.SetValue(transform.rotation);
        latestSafeRotation = initialRotation = transform.rotation;
        potentialUnsafeRotation = false;
    }

    void FixedUpdate()
    {
        SetSafePosition();
    }

    public void RotateWorld(Rotation rotation)
    {        
        Vector3 axis_vector = AxisUtils.AxisToVector(rotation.axis);
        worldRotation.SetValue(Quaternion.AngleAxis(rotation.direction.Equals(Direction.Clockwise) ? 90 : -90, worldRotation.Value * axis_vector) * worldRotation.Value);
        potentialUnsafeRotation = true;
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

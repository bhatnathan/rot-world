using UnityEngine;

public class WorldRotator : MonoBehaviour
{
    [SerializeField] private QuaternionVariable worldRotation;

    private Quaternion initialRotation;
    private Quaternion latestSafeRotation;

    private void Awake()
    {
        worldRotation.SetValue(transform.rotation);
        latestSafeRotation = initialRotation = transform.rotation;
    }

    public void RotateDesiredCamera(Axis axis, Rotation direction)
    {        
        Vector3 axis_vector = AxisUtils.AxisToVector(axis);
        worldRotation.SetValue(Quaternion.AngleAxis(direction.Equals(Rotation.Clockwise) ? 90 : -90, worldRotation.Value * axis_vector) * worldRotation.Value);
    }

}

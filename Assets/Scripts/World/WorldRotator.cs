using UnityEngine;

public class WorldRotator : MonoBehaviour
{
    [SerializeField] private QuaternionVariable worldRotation;    

    public void RotateDesiredCamera(Axis axis, Rotation direction)
    {
        Vector3 axis_vector = AxisUtils.AxisToVector(axis);
        worldRotation.SetValue(Quaternion.AngleAxis(direction.Equals(Rotation.Clockwise) ? 90 : -90, worldRotation.Value * axis_vector) * worldRotation.Value);
    }

}
